using ExpensesTracker.Models;

namespace ExpensesTracker.Data.Services
{
    public interface IExpensesService
    {
        Task<IEnumerable<Expense>> GetAll();
        Task Add(Expense expense);
        Task<bool> Edit(int Id, Expense expense);

        Task<Expense?> Edit(int Id);
        Task Delete(int Id);
        IQueryable GetChartData();
    }
}
