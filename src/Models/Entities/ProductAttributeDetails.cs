using System;
namespace Models.Entities
{
    public class ProductAttributeDetails
    {
        public int ProductAttributeId { get; set; }
        public string ProductName { get; set; }
        public string PropertyName  { get; set; }
        public string PropertyValue  { get; set; }
        public string UnitName  { get; set; }
        public decimal Mrp  { get; set; }
        public decimal Rate  { get; set; }
        public bool IsActive { get; set; }

    }
}