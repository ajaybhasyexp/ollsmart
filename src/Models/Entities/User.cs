using System;
namespace Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}