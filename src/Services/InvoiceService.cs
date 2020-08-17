using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ollsmart.Services
{
    public class InvoiceService : IInvoiceService
    {
        private OllsMartContext _dbContext;

        public InvoiceService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }
        public List<InvoiceHeader>  GetAll()
        {
            return _dbContext.InvoiceHeaders.OrderBy(x => x.InvoiceHeaderId).ToList();
         
        }   
    }
}
