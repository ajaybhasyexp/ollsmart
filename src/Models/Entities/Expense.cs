using System;
using System.Collections.Generic;
namespace Models.Entities
{
    public class Expense
    {
        public int ExpenseId{ get; set; }
        public int ExpenseHeadId { get; set; }
        public int UserId { get; set; }
        public DateTime TransDate { get; set; }
        public double   Amount { get; set; }
        public string   Remarks { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}