using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace ollsmart.Services
{
    public class OrderService : IOrderService
    {
        private OllsMartContext _dbContext;

        public OrderService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }
        public OrderHeader GetOrderById(int id)
        {
             return _dbContext.OrderHeaders.Include(o => o.OrderDetail).Where(o => o.OrderHeaderId==id).FirstOrDefault();    
        }
        public List<OrderDetailVM> GetOrderDetailsById(int id)
        {
            //  return _dbContext.OrderDetails.Where(o => o.OrderHeaderId==id).ToList();  
           return (from od in _dbContext.OrderDetails.Where(o => (o.OrderHeaderId==id))
                    join pa in _dbContext.ProductAttributes on od.ProductAttributeId equals pa.ProductAttributeId 
                    join p in _dbContext.Products on pa.ProductId equals p.ProductId
                  select new OrderDetailVM() { ProductAttributeId = pa.ProductAttributeId,  ProductName = p.ProductName, PropertyValue = pa.PropertyValue ,Rate=od.Rate,Quantity=od.Quantity, Amount=od.Amount,Discount=od.Discount}).OrderBy(o => o.ProductName).ToList();
              
        }
        public List<OrderHeader> GetOrderDetails(DateTime fromDate,DateTime toDate)
        {
             return _dbContext.OrderHeaders.Include(o => o.OrderDetail).Where(o => o.OrderDate.Date>= fromDate.Date &&  o.OrderDate.Date<= toDate.Date).ToList();    
        }
        public OrderHeader SaveOrder(OrderHeader orderData)
        {
            if (orderData != null)
            {
                if (orderData.OrderHeaderId == 0)
                {
                    orderData.TimeStamp = DateTime.UtcNow;
                    _dbContext.OrderHeaders.Add(orderData);
                }
                else
                {
                    orderData.TimeStamp = DateTime.UtcNow;
                    _dbContext.OrderHeaders.Update(orderData);
                }
                _dbContext.SaveChanges();
                return orderData;
            }
            else
            {
                throw new ArgumentNullException("Order");
            }
        }
        
    }
}
