using System;
using System.Collections.Generic;
namespace Models.Entities
{
    public class InvoiceHeader
    {
        public int InvoiceHeaderId { get; set; }
        public string InvoiceNo { get; set; }
        public int UserId  { get; set; }
        public DateTime InvoicedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public int Status  { get; set; }      
        public bool IsExpressDelivery { get; set; }
        public int DeliveryTimeSlotId { get; set; }
        public int SourceId { get; set; }
        public int UserAddressId { get; set; }
        public double TotalDiscount { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime DeliveredTime { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<InvoiceDetail> InvoiceDetail { get; set; } =new List<InvoiceDetail>();
        
    }
}