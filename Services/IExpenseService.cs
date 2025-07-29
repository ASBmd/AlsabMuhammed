using MonthlyExpenseTracker.Models;

namespace MonthlyExpenseTracker.Services
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetAllExpensesAsync();
        Task<Expense?> GetExpenseByIdAsync(int id);
        Task<List<Expense>> GetExpensesByMonthAsync(int year, int month);
        Task<List<Expense>> GetExpensesByCategoryAsync(ExpenseCategory category);
        Task<int> AddExpenseAsync(Expense expense);
        Task<bool> UpdateExpenseAsync(Expense expense);
        Task<bool> DeleteExpenseAsync(int id);
        Task<MonthlyReport> GenerateMonthlyReportAsync(int year, int month);
        Task<decimal> GetTotalExpensesAsync();
        Task<decimal> GetTotalExpensesByMonthAsync(int year, int month);
    }
}