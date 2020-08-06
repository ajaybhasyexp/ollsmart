using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

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
    }
}
