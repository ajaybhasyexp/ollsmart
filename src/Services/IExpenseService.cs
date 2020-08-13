using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface IExpenseService
    {
        List<ExpenseHead>  GetAllExpenseHeads();
        ExpenseHead GetExpenseHeadById(int id);
        ExpenseHead SaveExpenseHead(ExpenseHead expenseHead);
        Expense SaveExpense(Expense expense);
        List<ExpenseDetails> GetExpenseDetails(DateTime fromDate,DateTime toDate);
        Expense GetExpenseById(int id);
        bool DeleteExpense(Expense expense);

    }
}
