using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Taller.Src.Services;
using Taller.Src.Interfaces;
using Taller.Src.Data;
using Taller.Src.Helpers;
using Taller.Src.Models;
using Taller.Src.Requesthelpers;
using Taller.Src.Mappers;
using Taller.Src.Dtos;
using Taller.Src.Extensions;


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
                     .Sort(productParams.OrderBy);

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
    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        await _context.ProductRepository.AddProductAsync(product);
        await _context.SaveChangeAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);

    }
}