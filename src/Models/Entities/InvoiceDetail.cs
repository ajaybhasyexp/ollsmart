using System;
namespace Models.Entities
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductAttributeId { get; set; }
        public int Quantity  { get; set; }
        public double Rate  { get; set; }
        public double Discount { get; set; }
        public double Amount { get; set; }
        public int UnitId   { get; set; }
         
    }
}