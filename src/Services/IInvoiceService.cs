using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface IInvoiceService
    {
        List<InvoiceHeader> GetAll();
    }
}
