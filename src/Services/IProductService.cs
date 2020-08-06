using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface IProductService
    {
        Product SaveProduct(Product product);
        Product GetProductById(int id);
        List<ProductDetails> GetProducts();
        List<ProductProperty> GetProductProperty();
        ProductProperty GetProductPropertyById(int id);
        ProductProperty SaveProductProperty(ProductProperty productProperty);
        ProductAttribute SaveProductAttribute(ProductAttribute productAttribute);
    }
}
