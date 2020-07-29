using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface IBrandService
    {
        Brand SaveBrand(Brand brand);
        List<Brand> GetAll();
        Brand GetBrandById(int id);
        bool DeleteBrand(Brand brand);
    }
}
