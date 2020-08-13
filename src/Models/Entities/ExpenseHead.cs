using System;
using System.Collections.Generic;
namespace Models.Entities
{
    public class ExpenseHead
    {
        public int ExpenseHeadId { get; set; }
        public string ExpenseHeadName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}