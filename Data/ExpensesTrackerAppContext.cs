using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Data
{
    public class ExpensesTrackerAppContext: DbContext
    {
        public ExpensesTrackerAppContext(DbContextOptions<ExpensesTrackerAppContext> options) : base(options){ }
            public DbSet<Expense> Expenses { get; set; }
    }
}
