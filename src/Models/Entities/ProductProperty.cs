using System;
using System.Collections.Generic;
namespace Models.Entities
{
    public class ProductProperty
    {
        public int ProductPropertyId { get; set; }
        public string PropertyName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }   
       
    }
}