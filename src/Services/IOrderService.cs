using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface IOrderService
    {
        OrderHeader GetOrderById(int id);
        OrderHeader SaveOrder(OrderHeader orderData);
        List<OrderDetail> GetOrderDetailsById(int id);
        List<OrderHeaderDetail> GetOrderDetails(DateTime fromDate,DateTime toDate);
      
    }
}
