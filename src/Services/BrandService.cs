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
                    _dbContext.Brands.Add(brand);
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
