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
                     _dbContext.Products.Add(product);
                }
                _dbContext.SaveChanges();
                return product;
            }
            else
            {
                throw new ArgumentNullException("Product");
            }
            
        }
        public string GetProductById(int id)
        {
            return "111";
        }
    }
}
