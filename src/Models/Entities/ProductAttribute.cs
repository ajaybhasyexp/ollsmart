using System;
namespace Models.Entities
{
    public class ProductAttribute
    {
        public int ProductAttributeId { get; set; }
        public string ProductId { get; set; }
        public int PropertyId  { get; set; }
        public string PropertyValue  { get; set; }
        public int UnitId  { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}