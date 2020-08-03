using System;
namespace Models.Entities
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}