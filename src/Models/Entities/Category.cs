    using System;
    namespace Models.Entities
    {
        public class Category
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int ParentCategoryId  { get; set; }
            public string ImageUrl { get; set; }
            public bool IsActive { get; set; }
            public int CreatedBy { get; set; }
            public DateTime CreatedTime { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }