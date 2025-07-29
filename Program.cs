using MonthlyExpenseTracker.Services;
using MonthlyExpenseTracker.UI;

namespace MonthlyExpenseTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Initialize services
                var dataService = new JsonDataService("expenses.json");
                var expenseService = new ExpenseService(dataService);
                
                // Initialize the expense service (load data)
                await expenseService.InitializeAsync();
                
                // Create and run the console UI
                var consoleUI = new ConsoleUI(expenseService);
                await consoleUI.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}