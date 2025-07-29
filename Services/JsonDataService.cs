using Newtonsoft.Json;
using MonthlyExpenseTracker.Models;

namespace MonthlyExpenseTracker.Services
{
    public class JsonDataService
    {
        private readonly string _dataFilePath;
        
        public JsonDataService(string dataFilePath = "expenses.json")
        {
            _dataFilePath = dataFilePath;
        }
        
        public async Task<List<Expense>> LoadExpensesAsync()
        {
            try
            {
                if (!File.Exists(_dataFilePath))
                {
                    return new List<Expense>();
                }
                
                var json = await File.ReadAllTextAsync(_dataFilePath);
                var expenses = JsonConvert.DeserializeObject<List<Expense>>(json);
                return expenses ?? new List<Expense>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading expenses: {ex.Message}");
                return new List<Expense>();
            }
        }
        
        public async Task SaveExpensesAsync(List<Expense> expenses)
        {
            try
            {
                var json = JsonConvert.SerializeObject(expenses, Formatting.Indented);
                await File.WriteAllTextAsync(_dataFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving expenses: {ex.Message}");
                throw;
            }
        }
        
        public async Task<bool> BackupDataAsync()
        {
            try
            {
                if (!File.Exists(_dataFilePath))
                    return false;
                    
                var backupPath = $"{_dataFilePath}.backup.{DateTime.Now:yyyyMMdd_HHmmss}";
                File.Copy(_dataFilePath, backupPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating backup: {ex.Message}");
                return false;
            }
        }
    }
}