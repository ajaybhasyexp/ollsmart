using System;
namespace Models.Entities
{
    public class ProductAttribute
    {
        public int ProductAttributeId { get; set; }
        public int ProductId { get; set; }
        public int PropertyId  { get; set; }
        public string PropertyValue  { get; set; }
        public decimal UnitValue  { get; set; }
        public decimal Mrp  { get; set; }
        public decimal Rate  { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}