using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

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
             return _dbContext.OrderHeaders.Where(o => o.OrderHeaderId==id).FirstOrDefault();    
        }
        public List<OrderDetail> GetOrderDetailsById(int id)
        {
             return _dbContext.OrderDetails.Where(o => o.OrderHeaderId==id).ToList();    
        }
        public List<OrderHeaderDetail> GetOrderDetails(DateTime fromDate,DateTime toDate)
        {
            return (from oh in _dbContext.OrderHeaders
            join od in _dbContext.OrderDetails on oh.OrderHeaderId equals od.OrderDetailId
            where oh.OrderDate>= fromDate.Date && oh.OrderDate <= toDate.Date
            group od by new  { oh.OrderDate, oh.OrderHeaderId,oh.OrderNo,oh.Status,oh.ExpectedDeliveryDate } into g
            select new OrderHeaderDetail { 
                OrderDate = g.Key.OrderDate, 
                OrderHeaderId = g.Key.OrderHeaderId,
                OrderNo = g.Key.OrderNo,     
                Status = g.Key.Status,
                ExpectedDeliveryDate = g.Key.ExpectedDeliveryDate,
                TotalAmount = g.Sum(a => a.Amount),
                // LineItem=g.Count()
            }).ToList();
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
