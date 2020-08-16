using System;
using System.Collections.Generic;
namespace Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId  { get; set; }
        public int BrandId  { get; set; }
        public int UnitId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }   
        public List<ProductAttribute> ProductAttribute { get; set; } = new List<ProductAttribute>();
    }
}