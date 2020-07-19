using System;
namespace Models.Entities
{
    public class DeliveryTimeSlot
    {
        public int DeliveryTimeSlotId { get; set; }
        public String FromTime { get; set; }
        public String ToTime { get; set; }
        public String DeliverySlot  { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}