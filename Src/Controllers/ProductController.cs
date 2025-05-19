using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Taller.Src.Data;
using Taller.Src.Dtos;
using Taller.Src.Extensions;
using Taller.Src.Helpers;
using Taller.Src.Interfaces;
using Taller.Src.Mappers;
using Taller.Src.Models;
using Taller.Src.Requesthelpers;
using Taller.Src.Services;


namespace Taller.Src.Controllers;

public class ProductController(ILogger<ProductController> logger, UnitOfWork unitOfWork, IPhotoService photoService) : BaseController
{
    private readonly ILogger<ProductController> _logger = logger;
    private readonly UnitOfWork _context = unitOfWork;
    private readonly IPhotoService _photoService = photoService;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Product>>>> GetPaged([FromQuery] ProductParams productParams)
    {
        var query = _context.ProductRepository.GetQueryableProducts();

        query = query.Search(productParams.Search)
                     .Filter(productParams.Brands, productParams.Categories)
                     .Sort(productParams.OrderBy)
                     .FilterByCondition(productParams.Condition);

        var pagedList = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

        if (pagedList == null || pagedList.Count == 0)
            return Ok(new ApiResponse<IEnumerable<Product>>(false, "No hay productos disponibles"));


        Response.AddPaginationHeader(pagedList.Metadata);

        return Ok(new ApiResponse<IEnumerable<Product>>(
            true,
            "Productos obtenidos correctamente",
            pagedList
        ));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Product>>> GetById(int id)
    {
        var product = await _context.ProductRepository.GetProductByIdAsync(id);
        return product == null
            ? (ActionResult<ApiResponse<Product>>)NotFound(new ApiResponse<Product>(false, "Product not found"))
            : (ActionResult<ApiResponse<Product>>)Ok(new ApiResponse<Product>(true, "Product retrieved successfully", product));
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<Product>>> Create([FromForm] ProductDto productDto)
    {
        var urls = new List<string>();
        string? publicId = null;

        foreach (var image in productDto.Images)
        {
            var result = await _photoService.UploadImageAsync(image);
            if (result.Error != null)
                return BadRequest(new ApiResponse<Product>(false, result.Error.Message, null, new List<string> { result.Error.Message }));
            urls.Add(result.SecureUrl.AbsoluteUri);
            publicId = result.PublicId;
        }

        var product = ProductMapper.FromCreateDto(productDto, urls, publicId);
        await _context.ProductRepository.AddProductAsync(product);
        await _context.SaveChangeAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, new ApiResponse<Product>(true, "Product created successfully", product));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<Product>>> Update(int id, [FromForm] ProductDto dto)
    {
        var product = await _context.ProductRepository.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new ApiResponse<Product>(false, "Producto no encontrado"));


        if (dto.Images.Any())
        {
            // Eliminar TODAS las imágenes anteriores usando las URLs
            if (product.Urls != null && product.Urls.Any())
            {
                foreach (var url in product.Urls)
                {
                    var oldPublicId = CloudinaryHelper.ExtractPublicIdFromUrl(url);
                    if (!string.IsNullOrEmpty(oldPublicId))
                        await _photoService.DeleteImageAsync(oldPublicId);
                }
            }

            // Subir la nueva imagen (puedes extender a varias si lo permites)
            var result = await _photoService.UploadImageAsync(dto.Images.First());
            if (result.Error != null)
            {
                return BadRequest(new ApiResponse<Product>(
                    false,
                    "Error al subir nueva imagen",
                    null,
                    new List<string> { result.Error.Message }
                ));
            }

            product.Urls = new List<string> { result.SecureUrl.AbsoluteUri };
            product.PublicId = result.PublicId;
        }

        // Actualizar datos básicos
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Category = dto.Category;
        product.Brand = dto.Brand;
        product.Stock = dto.Stock;
        product.Condition = dto.Condition;

        await _context.ProductRepository.UpdateProductAsync(product);
        await _context.SaveChangeAsync();

        return Ok(new ApiResponse<Product>(true, "Producto actualizado correctamente", product));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<Product>>> Delete(int id)
    {
        var product = await _context.ProductRepository.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new ApiResponse<Product>(false, "Producto no encontrado"));

        // Eliminar imagen de Cloudinary
        if (!string.IsNullOrEmpty(product.PublicId))
            await _photoService.DeleteImageAsync(product.PublicId);

        await _context.ProductRepository.DeleteProductAsync(product);
        await _context.SaveChangeAsync();

        return Ok(new ApiResponse<Product>(true, "Producto eliminado correctamente", product));
    }


}