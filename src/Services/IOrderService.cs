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
        List<OrderDetailVM> GetOrderDetailsById(int id);
        List<OrderHeader> GetOrderDetails(DateTime fromDate,DateTime toDate);
      
    }
}
