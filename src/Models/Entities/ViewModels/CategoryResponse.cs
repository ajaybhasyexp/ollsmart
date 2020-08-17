    using System;
    namespace Models.Entities
    {
        public class CategoryResponse
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public int ParentCategoryId  { get; set; }
            public string ParentCategory  { get; set; }
            public string ImageUrl { get; set; }
            public bool IsActive { get; set; }
        }
    }