using MonthlyExpenseTracker.Models;

namespace MonthlyExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly JsonDataService _dataService;
        private List<Expense> _expenses;
        private int _nextId;
        
        public ExpenseService(JsonDataService dataService)
        {
            _dataService = dataService;
            _expenses = new List<Expense>();
            _nextId = 1;
        }
        
        public async Task InitializeAsync()
        {
            _expenses = await _dataService.LoadExpensesAsync();
            _nextId = _expenses.Any() ? _expenses.Max(e => e.Id) + 1 : 1;
        }
        
        public async Task<List<Expense>> GetAllExpensesAsync()
        {
            return _expenses.OrderByDescending(e => e.Date).ToList();
        }
        
        public async Task<Expense?> GetExpenseByIdAsync(int id)
        {
            return _expenses.FirstOrDefault(e => e.Id == id);
        }
        
        public async Task<List<Expense>> GetExpensesByMonthAsync(int year, int month)
        {
            return _expenses
                .Where(e => e.Date.Year == year && e.Date.Month == month)
                .OrderByDescending(e => e.Date)
                .ToList();
        }
        
        public async Task<List<Expense>> GetExpensesByCategoryAsync(ExpenseCategory category)
        {
            return _expenses
                .Where(e => e.Category == category)
                .OrderByDescending(e => e.Date)
                .ToList();
        }
        
        public async Task<int> AddExpenseAsync(Expense expense)
        {
            expense.Id = _nextId++;
            _expenses.Add(expense);
            await _dataService.SaveExpensesAsync(_expenses);
            return expense.Id;
        }
        
        public async Task<bool> UpdateExpenseAsync(Expense expense)
        {
            var existingExpense = _expenses.FirstOrDefault(e => e.Id == expense.Id);
            if (existingExpense == null)
                return false;
                
            var index = _expenses.IndexOf(existingExpense);
            _expenses[index] = expense;
            await _dataService.SaveExpensesAsync(_expenses);
            return true;
        }
        
        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var expense = _expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
                return false;
                
            _expenses.Remove(expense);
            await _dataService.SaveExpensesAsync(_expenses);
            return true;
        }
        
        public async Task<MonthlyReport> GenerateMonthlyReportAsync(int year, int month)
        {
            var monthlyExpenses = await GetExpensesByMonthAsync(year, month);
            
            var report = new MonthlyReport
            {
                Year = year,
                Month = month,
                TotalExpenses = monthlyExpenses.Sum(e => e.Amount),
                TotalTransactions = monthlyExpenses.Count,
                AverageExpenseAmount = monthlyExpenses.Any() ? monthlyExpenses.Average(e => e.Amount) : 0,
                TopExpenses = monthlyExpenses.OrderByDescending(e => e.Amount).Take(5).ToList()
            };
            
            // Group by category
            report.ExpensesByCategory = monthlyExpenses
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
                
            // Group by payment method
            report.ExpensesByPaymentMethod = monthlyExpenses
                .GroupBy(e => e.PaymentMethod)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
                
            return report;
        }
        
        public async Task<decimal> GetTotalExpensesAsync()
        {
            return _expenses.Sum(e => e.Amount);
        }
        
        public async Task<decimal> GetTotalExpensesByMonthAsync(int year, int month)
        {
            var monthlyExpenses = await GetExpensesByMonthAsync(year, month);
            return monthlyExpenses.Sum(e => e.Amount);
        }
    }
}