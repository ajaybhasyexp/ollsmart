using System;
namespace Models.Entities
{
    public class OrderHeader
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public int UserId  { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status  { get; set; }      
        public DateTime OrderTransactionTime { get; set; }
        public int DeliveryTimeSlotId { get; set; }
        public bool IsDelivered { get; set; }
         
    }
}