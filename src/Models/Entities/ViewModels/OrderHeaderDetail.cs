using System;
using System.Collections.Generic;
namespace Models.Entities
{
    public class OrderHeaderDetail
    {
        public int OrderHeaderId { get; set; }
        public string OrderNo { get; set; }
        public int UserId  { get; set; }
        public String UserName  { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public int Status  { get; set; }      
        public DateTime TimeStamp { get; set; }
        public bool IsExpressDelivery { get; set; }
        public int DeliveryTimeSlotId { get; set; }
        public string DeliveryTimeSlot { get; set; }
        public int SourceId { get; set; }
        public string Source{ get; set; }
        public int UserAddressId { get; set; }
        public double TotalAmount { get; set; }
        public int LineItem { get; set; }

        
    }
}