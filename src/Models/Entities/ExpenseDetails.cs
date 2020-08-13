using System;
namespace Models.Entities
{
    public class ExpenseDetails
    {
        public int ExpenseId{ get; set; }
        public int ExpenseHeadId { get; set; }
        public string ExpenseHeadName { get; set; }
        public int UserId { get; set; }
        public DateTime TransDate { get; set; }
        public double   Amount { get; set; }
        public string   Remarks { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}