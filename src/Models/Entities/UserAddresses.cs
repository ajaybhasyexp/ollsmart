using System;
namespace Models.Entities
{
    public class UserAddress
    {
        public int UserAddressId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsPrimary { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}