using Models.Entities;
using OllsMart;
using System;

namespace ollsmart.Services
{
    public class CategoryService : ICategoryService
    {
        private OllsMartContext _dbContext;

        public CategoryService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }

        public Category SaveCategory(Category category)
        {
            if (category != null)
            {
                if (category.CategoryId == 0)
                {
                    category.Timestamp = DateTime.UtcNow;
                    category.CreatedTime = DateTime.UtcNow;
                    _dbContext.Categories.Add(category);
                }
                else
                {
                    category.Timestamp = DateTime.UtcNow;
                    _dbContext.Categories.Add(category);
                }
                _dbContext.SaveChanges();
                return category;
            }
            else
            {
                throw new ArgumentNullException("Category");
            }
            
        }
    }
}
