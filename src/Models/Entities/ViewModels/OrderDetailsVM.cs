using System;
namespace Models.Entities
{
    public class OrderDetailVM
    {
        public int OrderDetailId { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductAttributeId { get; set; }
        public string ProductName { get; set; }
        public string PropertyValue { get; set; }
        public int Quantity  { get; set; }
        public double Rate  { get; set; }
        public double Discount { get; set; }
        public double Amount { get; set; }
        public int UnitId   { get; set; }
        public string UnitName   { get; set; }
         
    }
}