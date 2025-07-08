using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Data.Services
{
    public class ExpensesService(ExpensesTrackerAppContext context) : IExpensesService
    {
        private readonly ExpensesTrackerAppContext _context = context;
        public async Task Add(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var data = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == Id);
            if (data != null)
            {
                _context.Expenses.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Expense?> Edit(int Id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == Id);
            return expense;
        }
        public async Task<bool> Edit(int Id, Expense expense)
        {
            var data = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == Id);
            if (data != null)
            {
                data.Description = expense.Description;
                data.Amount = expense.Amount;
                data.Category = expense.Category;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            var expenses = await _context.Expenses.ToListAsync();
            return expenses;
        }

        public IQueryable GetChartData()
        {
            var chartData = _context.Expenses
                .GroupBy(x => x.Category)
                .Select(g => new
                {
                    category = g.Key,
                    totalAmount = g.Sum(x => x.Amount)
                });
            return chartData;
        }
    }
}
