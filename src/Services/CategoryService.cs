using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ollsmart.Services
{
    public class CategoryService : ICategoryService
    {
        private OllsMartContext _dbContext;

        public CategoryService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }
        public List<CategoryResponse>  GetAll()
        {
            return (from c in _dbContext.Categories
                    join p in _dbContext.Categories on c.ParentCategoryId equals p.CategoryId 
                    into g from p in g.DefaultIfEmpty()
                  select new CategoryResponse() { CategoryId = c.CategoryId,  CategoryName = c.CategoryName, ParentCategoryId = c.ParentCategoryId,
                  ParentCategory = p.CategoryName,IsActive = c.IsActive,ImageUrl=c.ImageUrl,Description = c.Description }).OrderBy(x => x.ParentCategoryId).ThenBy(x => x.CategoryName).ToList();
         
        }  
        public List<CategoryResponse> GetParentCategory()
        {
            return (from c in _dbContext.Categories
                    join p in _dbContext.Categories on c.ParentCategoryId equals p.CategoryId 
                    into g from p in g.DefaultIfEmpty()
                  select new CategoryResponse() { CategoryId = c.CategoryId,  CategoryName = c.CategoryName, ParentCategoryId = c.ParentCategoryId,
                  ParentCategory = p.CategoryName,IsActive = c.IsActive,ImageUrl=c.ImageUrl,Description = c.Description }).OrderBy(x => x.ParentCategoryId).ThenBy(x => x.CategoryName).Where(o => (o.ParentCategoryId==0)).ToList();
          //  return data.where(o => o.ParentCategoryId>0);
         
        } 
        public List<CategoryResponse> GetSubCategory(int id)
        {
            if(id==0) {
                return (from c in _dbContext.Categories
                    join p in _dbContext.Categories on c.ParentCategoryId equals p.CategoryId 
                    into g from p in g.DefaultIfEmpty()
                  select new CategoryResponse() { CategoryId = c.CategoryId,  CategoryName = c.CategoryName, ParentCategoryId = c.ParentCategoryId,
                  ParentCategory = p.CategoryName,IsActive = c.IsActive,ImageUrl=c.ImageUrl,Description = c.Description }).OrderBy(x => x.ParentCategoryId).ThenBy(x => x.CategoryName).Where(o => (o.ParentCategoryId!=0)).ToList();
            }else {
                  return (from c in _dbContext.Categories
                    join p in _dbContext.Categories on c.ParentCategoryId equals p.CategoryId 
                    into g from p in g.DefaultIfEmpty()
                  select new CategoryResponse() { CategoryId = c.CategoryId,  CategoryName = c.CategoryName, ParentCategoryId = c.ParentCategoryId,
                  ParentCategory = p.CategoryName,IsActive = c.IsActive,ImageUrl=c.ImageUrl,Description = c.Description }).OrderBy(x => x.ParentCategoryId).ThenBy(x => x.CategoryName).Where(o => (o.ParentCategoryId==id)).ToList();
            }
          
         
        } 
        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories.Where(o => o.CategoryId == id).FirstOrDefault();
         
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
                    _dbContext.Categories.Update(category);
                }
                _dbContext.SaveChanges();
                return category;
            }
            else
            {
                throw new ArgumentNullException("Category");
            }
            
        }
        public bool DeleteCategory(Category category)
        {
            if (category != null)
            {              
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new ArgumentNullException("Delete Category");
            }
         
        }  
    }
}
