using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ollsmart.Services
{
    public class ProductService : IProductService
    {
        private OllsMartContext _dbContext;

        public ProductService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }

        public Product SaveProduct(Product product)
        {
            if (product != null)
            {
                if (product.ProductId == 0)
                {
                    product.Timestamp = DateTime.UtcNow;
                    product.CreatedTime = DateTime.UtcNow;
                    _dbContext.Products.Add(product);
                }
                else
                {
                    product.Timestamp = DateTime.UtcNow;
                     _dbContext.Products.Update(product);
                }
                _dbContext.SaveChanges();
                return product;
            }
            else
            {
                throw new ArgumentNullException("Product");
            }
            
        }
        public List<ProductDetails> GetProducts()
        {
             return (from p in _dbContext.Products
                    join c in _dbContext.Categories on p.CategoryId equals c.CategoryId 
					join b in _dbContext.Brands on p.BrandId equals b.BrandId 
                  select new ProductDetails() { ProductId = p.ProductId,  ProductName = p.ProductName, Description = p.Description ,Brand=b.BrandName,
                  Category = c.CategoryName,IsActive = p.IsActive,ImageUrl=p.ImageUrl}).OrderBy(x => x.ProductName).ThenBy(x => x.Category).ToList();
            
        }
        public Product GetProductById(int id)
        {
            return _dbContext.Products.Where(o => o.ProductId==id).FirstOrDefault();        
        }
        public List<ProductProperty> GetProductProperty()
        {
            return _dbContext.ProductProperties.OrderBy(x => x.PropertyName).ToList();
        }
         public ProductProperty GetProductPropertyById(int id)
        {
              return _dbContext.ProductProperties.Where(o => o.ProductPropertyId==id).FirstOrDefault();        
        }
        public ProductProperty SaveProductProperty(ProductProperty productProperty)
        {
            if (productProperty != null)
            {
                if (productProperty.ProductPropertyId == 0)
                {
                    productProperty.Timestamp = DateTime.UtcNow;
                    productProperty.CreatedTime = DateTime.UtcNow;
                    _dbContext.ProductProperties.Add(productProperty);
                }
                else
                {
                    productProperty.Timestamp = DateTime.UtcNow;
                     _dbContext.ProductProperties.Update(productProperty);
                }
                _dbContext.SaveChanges();
                return productProperty;
            }
            else
            {
                throw new ArgumentNullException("Product Property");
            }
            
        }
        public ProductAttribute SaveProductAttribute(ProductAttribute productAttribute)
        {
            if (productAttribute != null)
            {
                if (productAttribute.ProductAttributeId == 0)
                {
                    productAttribute.Timestamp = DateTime.UtcNow;
                    productAttribute.CreatedTime = DateTime.UtcNow;
                    _dbContext.ProductAttributes.Add(productAttribute);
                }
                else
                {
                    productAttribute.Timestamp = DateTime.UtcNow;
                     _dbContext.ProductAttributes.Update(productAttribute);
                }
                _dbContext.SaveChanges();
                return productAttribute;
            }
            else
            {
                throw new ArgumentNullException("Product Attribute");
            }
            
        }
        public ProductAttribute GetProductAttributeById(int id)
        {
            return _dbContext.ProductAttributes.Where(o => o.ProductAttributeId==id).FirstOrDefault();        
        }
         public List<ProductAttributeDetails> GetProductAttributes()
        {
             return (from pa in _dbContext.ProductAttributes
                    join p in _dbContext.Products on pa.ProductId equals p.ProductId 
					join pp in _dbContext.ProductProperties on pa.PropertyId equals pp.ProductPropertyId 
                    join u in _dbContext.Units on pa.UnitId equals u.UnitId 
                  select new ProductAttributeDetails() { ProductAttributeId = pa.ProductAttributeId,  ProductName = p.ProductName, PropertyName = pp.PropertyName ,UnitName=u.UnitName,PropertyValue=pa.PropertyValue,
                  Mrp=pa.Mrp,Rate=pa.Rate,
                  IsActive = pa.IsActive}).OrderBy(x => x.ProductName).ThenBy(x => x.PropertyValue).ToList();
            
        }
        public List<Product> GetProductList(int skip ,int take ,int parentCategoryId, int subCategoryId, string productName)
        {
            
           return _dbContext.Products.Include(p=>p.ProductAttribute).Where(b=>b.IsActive==true && b.ProductAttribute.Any() && (subCategoryId==0|| b.CategoryId== subCategoryId )&& (string.IsNullOrEmpty(productName)|| b.ProductName.ToLower().Contains(productName.ToLower()))).OrderBy(x => x.ProductName).Skip(skip).Take(take).ToList();
           
        }
    }
}



