using System;
using System.Collections.Generic;
namespace Models.Entities
{
    public class ProductDetails
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId  { get; set; }
        public string Category  { get; set; }
        public int BrandId  { get; set; }
        public string Brand  { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}