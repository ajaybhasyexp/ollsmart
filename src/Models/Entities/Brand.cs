using System;
namespace Models.Entities
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}