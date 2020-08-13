using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ollsmart.Services
{
    public class ExpenseService : IExpenseService
    {
        private OllsMartContext _dbContext;

        public ExpenseService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }
        
        public List<ExpenseHead>  GetAllExpenseHeads()
        {
            return _dbContext.ExpenseHeads.OrderBy(x => x.ExpenseHeadName).ToList();
         
        }   
        public ExpenseHead GetExpenseHeadById(int id)
        {
            return _dbContext.ExpenseHeads.Where(o => o.ExpenseHeadId==id).FirstOrDefault();
         
        }
        public ExpenseHead SaveExpenseHead(ExpenseHead expenseHead)
        {
            if(expenseHead!=null)
            {
                if(expenseHead.ExpenseHeadId==0)
                {
                    expenseHead.Timestamp = DateTime.UtcNow;
                    expenseHead.CreatedTime = DateTime.UtcNow;
                    _dbContext.ExpenseHeads.Add(expenseHead);
                }
                else 
                {
                     expenseHead.Timestamp = DateTime.UtcNow;
                    _dbContext.ExpenseHeads.Update(expenseHead);
                }
                _dbContext.SaveChanges();
                return expenseHead;
            }
             else
            {
               throw new ArgumentNullException("SaveExpenseHead");
            }
         
        } 
        
        public Expense SaveExpense(Expense expense)
        {
            if(expense!=null)
            {
                if(expense.ExpenseId==0)
                {
                    expense.Timestamp = DateTime.UtcNow;
                    expense.CreatedTime = DateTime.UtcNow;
                    _dbContext.Expenses.Add(expense);
                }
                else 
                {
                     expense.Timestamp = DateTime.UtcNow;
                    _dbContext.Expenses.Update(expense);
                }
                _dbContext.SaveChanges();
                return expense;
            }
             else
            {
               throw new ArgumentNullException("SaveExpenseHead");
            }         
        } 
        public List<ExpenseDetails> GetExpenseDetails(DateTime fromDate,DateTime toDate)
        {
            // return _dbContext.Expenses.Where(t => t.TransDate >= fromDate.Date && t.TransDate <= toDate.Date).ToList();
             return (from e in _dbContext.Expenses
             join eh in _dbContext.ExpenseHeads on e.ExpenseHeadId equals eh.ExpenseHeadId 
                  select new ExpenseDetails() { ExpenseId = e.ExpenseId,  ExpenseHeadId = e.ExpenseHeadId, ExpenseHeadName = eh.ExpenseHeadName,
                  Amount = e.Amount,Remarks = e.Remarks,TransDate=e.TransDate }).OrderBy(x => x.TransDate).Where(t => (t.TransDate >= fromDate.Date && t.TransDate <= toDate.Date)).ToList();

        }
        public Expense GetExpenseById(int id)
        {
            return _dbContext.Expenses.Where(o => o.ExpenseId==id).FirstOrDefault();         
        }
        public bool DeleteExpense(Expense expense)
        {
            if (expense != null)
            {              
                _dbContext.Expenses.Remove(expense);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                throw new ArgumentNullException("Delete Category");
            }
         
        }  
       
    }
}
