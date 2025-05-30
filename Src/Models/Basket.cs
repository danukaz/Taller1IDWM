using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.Src.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public required string BasketId { get; set; }

        public List<BasketItem> Items { get; set; } = [];

        public void AddItem(Product product, int quantity)
        {
            if (product == null) ArgumentNullException.ThrowIfNull(product);
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0 "
                , nameof(quantity));

            var existingItem = FindItem(product.Id);

            if (existingItem == null)
            {
                Items.Add(new BasketItem
                {
                    Quantity = quantity,
                    Product = product
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

        }
        public void RemoveItem(int productId, int quantity)
        {

            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0 "
                , nameof(quantity));
            var item = FindItem(productId);

            if (item == null) return;

            item.Quantity -= quantity;

            if (item.Quantity <= 0)
            {
                Items.Remove(item);
            }

        }

        private BasketItem? FindItem(int productId)
        {
            return Items.FirstOrDefault(item => item.ProductId == productId);
        }
    }
}