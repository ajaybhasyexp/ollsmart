using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ollsmart.Services
{
    public class UnitService : IUnitService
    {
        private OllsMartContext _dbContext;

        public UnitService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }
        public List<Unit>  GetAll()
        {
           return _dbContext.Units.OrderBy(x => x.UnitName).ToList();
         
        } 
        public Unit GetUnitById(int id)
        {
            return _dbContext.Units.Where(o => o.UnitId==id).FirstOrDefault();        
        }  
        public Unit SaveUnit(Unit unit)
        {
            if (unit != null)
            {
                if (unit.UnitId == 0)
                {
                    unit.Timestamp = DateTime.UtcNow;
                    unit.CreatedTime = DateTime.UtcNow;
                    _dbContext.Units.Add(unit);
                }
                else
                {
                    unit.Timestamp = DateTime.UtcNow;
                     _dbContext.Units.Update(unit);
                }
                _dbContext.SaveChanges();
                return unit;
            }
            else
            {
                throw new ArgumentNullException("Unit");
            }
        }
        
    }
}
