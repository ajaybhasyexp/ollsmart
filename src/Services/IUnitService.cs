using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface IUnitService
    {
        Unit SaveUnit(Unit unit);
        List<Unit> GetAll();
        Unit GetUnitById(int id);
    }
}
