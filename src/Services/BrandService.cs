using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ollsmart.Services
{
    public class BrandService : IBrandService
    {
        private OllsMartContext _dbContext;

        public BrandService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }
        public List<Brand>  GetAll()
        {
            return _dbContext.Brands.ToList();
          //  return data.where(o => o.ParentCategoryId>0);
         
        }   
         public Brand GetBrandById(int id)

        {
            return _dbContext.Brands.Where(o => o.BrandId==id).FirstOrDefault();
          //  return data.where(o => o.ParentCategoryId>0);
         
        } 
        public bool DeleteBrand(Brand brand)
        {
            if (brand != null)
            {              
                _dbContext.Brands.Remove(brand);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new ArgumentNullException("Brand");
            }
         
        }    
        public Brand SaveBrand(Brand brand)
        {
            if (brand != null)
            {
                if (brand.BrandId == 0)
                {
                    brand.Timestamp = DateTime.UtcNow;
                    brand.CreatedTime = DateTime.UtcNow;
                    _dbContext.Brands.Add(brand);
                }
                else
                {
                    brand.Timestamp = DateTime.UtcNow;
                     _dbContext.Brands.Update(brand);
                }
                _dbContext.SaveChanges();
                return brand;
            }
            else
            {
                throw new ArgumentNullException("Brand");
            }
            
        }
    }
}
