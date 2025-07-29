using MonthlyExpenseTracker.Models;
using MonthlyExpenseTracker.Services;

namespace MonthlyExpenseTracker.UI
{
    public class ConsoleUI
    {
        private readonly IExpenseService _expenseService;
        
        public ConsoleUI(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        
        public async Task RunAsync()
        {
            Console.Clear();
            ShowWelcomeMessage();
            
            while (true)
            {
                ShowMainMenu();
                var choice = Console.ReadLine();
                
                try
                {
                    switch (choice)
                    {
                        case "1":
                            await AddExpenseAsync();
                            break;
                        case "2":
                            await ViewAllExpensesAsync();
                            break;
                        case "3":
                            await ViewExpensesByMonthAsync();
                            break;
                        case "4":
                            await ViewExpensesByCategoryAsync();
                            break;
                        case "5":
                            await UpdateExpenseAsync();
                            break;
                        case "6":
                            await DeleteExpenseAsync();
                            break;
                        case "7":
                            await GenerateMonthlyReportAsync();
                            break;
                        case "8":
                            await ShowSummaryAsync();
                            break;
                        case "9":
                            Console.WriteLine("Thank you for using Monthly Expense Tracker!");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        private void ShowWelcomeMessage()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  MONTHLY EXPENSE TRACKER                    ║");
            Console.WriteLine("║                     Version 1.0                             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }
        
        private void ShowMainMenu()
        {
            Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                        MAIN MENU                           │");
            Console.WriteLine("├─────────────────────────────────────────────────────────────┤");
            Console.WriteLine("│ 1. Add New Expense                                         │");
            Console.WriteLine("│ 2. View All Expenses                                       │");
            Console.WriteLine("│ 3. View Expenses by Month                                  │");
            Console.WriteLine("│ 4. View Expenses by Category                               │");
            Console.WriteLine("│ 5. Update Expense                                          │");
            Console.WriteLine("│ 6. Delete Expense                                          │");
            Console.WriteLine("│ 7. Generate Monthly Report                                 │");
            Console.WriteLine("│ 8. Show Summary                                            │");
            Console.WriteLine("│ 9. Exit                                                    │");
            Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
            Console.Write("Please select an option (1-9): ");
        }
        
        private async Task AddExpenseAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ ADD NEW EXPENSE ═══\n");
            
            var expense = new Expense();
            
            // Get description
            Console.Write("Description: ");
            expense.Description = Console.ReadLine() ?? "";
            
            // Get amount
            Console.Write("Amount ($): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                return;
            }
            expense.Amount = amount;
            
            // Get date
            Console.Write("Date (YYYY-MM-DD) or press Enter for today: ");
            var dateInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dateInput))
            {
                expense.Date = DateTime.Today;
            }
            else if (!DateTime.TryParse(dateInput, out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }
            else
            {
                expense.Date = date;
            }
            
            // Get category
            expense.Category = SelectCategory();
            
            // Get payment method
            expense.PaymentMethod = SelectPaymentMethod();
            
            // Get notes
            Console.Write("Notes (optional): ");
            expense.Notes = Console.ReadLine();
            
            // Get recurring status
            Console.Write("Is this a recurring expense? (y/n): ");
            expense.IsRecurring = Console.ReadLine()?.ToLower().StartsWith("y") == true;
            
            var id = await _expenseService.AddExpenseAsync(expense);
            Console.WriteLine($"\n✓ Expense added successfully with ID: {id}");
        }
        
        private async Task ViewAllExpensesAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ ALL EXPENSES ═══\n");
            
            var expenses = await _expenseService.GetAllExpensesAsync();
            
            if (!expenses.Any())
            {
                Console.WriteLine("No expenses found.");
                return;
            }
            
            Console.WriteLine($"{"ID",-4} {"Date",-12} {"Category",-15} {"Description",-25} {"Amount",-10} {"Payment",-12}");
            Console.WriteLine(new string('─', 80));
            
            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Id,-4} {expense.Date:yyyy-MM-dd,-12} {expense.Category,-15} " +
                                $"{TruncateString(expense.Description, 25),-25} ${expense.Amount,-9:F2} {expense.PaymentMethod,-12}");
            }
            
            Console.WriteLine($"\nTotal Expenses: ${expenses.Sum(e => e.Amount):F2}");
        }
        
        private async Task ViewExpensesByMonthAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ EXPENSES BY MONTH ═══\n");
            
            Console.Write("Enter year (YYYY): ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year.");
                return;
            }
            
            Console.Write("Enter month (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
            {
                Console.WriteLine("Invalid month.");
                return;
            }
            
            var expenses = await _expenseService.GetExpensesByMonthAsync(year, month);
            
            if (!expenses.Any())
            {
                Console.WriteLine($"No expenses found for {new DateTime(year, month, 1):MMMM yyyy}.");
                return;
            }
            
            Console.WriteLine($"\nExpenses for {new DateTime(year, month, 1):MMMM yyyy}:");
            Console.WriteLine($"{"ID",-4} {"Date",-12} {"Category",-15} {"Description",-25} {"Amount",-10}");
            Console.WriteLine(new string('─', 68));
            
            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Id,-4} {expense.Date:yyyy-MM-dd,-12} {expense.Category,-15} " +
                                $"{TruncateString(expense.Description, 25),-25} ${expense.Amount,-9:F2}");
            }
            
            Console.WriteLine($"\nTotal for {new DateTime(year, month, 1):MMMM yyyy}: ${expenses.Sum(e => e.Amount):F2}");
        }
        
        private async Task ViewExpensesByCategoryAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ EXPENSES BY CATEGORY ═══\n");
            
            var category = SelectCategory();
            var expenses = await _expenseService.GetExpensesByCategoryAsync(category);
            
            if (!expenses.Any())
            {
                Console.WriteLine($"No expenses found for category: {category}");
                return;
            }
            
            Console.WriteLine($"\nExpenses for category: {category}");
            Console.WriteLine($"{"ID",-4} {"Date",-12} {"Description",-30} {"Amount",-10}");
            Console.WriteLine(new string('─', 58));
            
            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Id,-4} {expense.Date:yyyy-MM-dd,-12} " +
                                $"{TruncateString(expense.Description, 30),-30} ${expense.Amount,-9:F2}");
            }
            
            Console.WriteLine($"\nTotal for {category}: ${expenses.Sum(e => e.Amount):F2}");
        }
        
        private async Task UpdateExpenseAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ UPDATE EXPENSE ═══\n");
            
            Console.Write("Enter expense ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                Console.WriteLine("Expense not found.");
                return;
            }
            
            Console.WriteLine($"\nCurrent expense details:");
            Console.WriteLine($"Description: {expense.Description}");
            Console.WriteLine($"Amount: ${expense.Amount:F2}");
            Console.WriteLine($"Date: {expense.Date:yyyy-MM-dd}");
            Console.WriteLine($"Category: {expense.Category}");
            Console.WriteLine($"Payment Method: {expense.PaymentMethod}");
            Console.WriteLine($"Notes: {expense.Notes ?? "None"}");
            
            Console.WriteLine("\nEnter new values (press Enter to keep current value):");
            
            Console.Write($"Description [{expense.Description}]: ");
            var newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
                expense.Description = newDescription;
            
            Console.Write($"Amount [{expense.Amount:F2}]: ");
            var amountInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(amountInput) && decimal.TryParse(amountInput, out decimal newAmount))
                expense.Amount = newAmount;
            
            Console.Write($"Date [{expense.Date:yyyy-MM-dd}]: ");
            var dateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
                expense.Date = newDate;
            
            Console.Write("Update category? (y/n): ");
            if (Console.ReadLine()?.ToLower().StartsWith("y") == true)
                expense.Category = SelectCategory();
            
            Console.Write("Update payment method? (y/n): ");
            if (Console.ReadLine()?.ToLower().StartsWith("y") == true)
                expense.PaymentMethod = SelectPaymentMethod();
            
            Console.Write($"Notes [{expense.Notes ?? "None"}]: ");
            var newNotes = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newNotes))
                expense.Notes = newNotes;
            
            if (await _expenseService.UpdateExpenseAsync(expense))
            {
                Console.WriteLine("\n✓ Expense updated successfully!");
            }
            else
            {
                Console.WriteLine("\n✗ Failed to update expense.");
            }
        }
        
        private async Task DeleteExpenseAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ DELETE EXPENSE ═══\n");
            
            Console.Write("Enter expense ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                Console.WriteLine("Expense not found.");
                return;
            }
            
            Console.WriteLine($"\nExpense to delete:");
            Console.WriteLine($"ID: {expense.Id}");
            Console.WriteLine($"Description: {expense.Description}");
            Console.WriteLine($"Amount: ${expense.Amount:F2}");
            Console.WriteLine($"Date: {expense.Date:yyyy-MM-dd}");
            
            Console.Write("\nAre you sure you want to delete this expense? (y/n): ");
            if (Console.ReadLine()?.ToLower().StartsWith("y") != true)
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }
            
            if (await _expenseService.DeleteExpenseAsync(id))
            {
                Console.WriteLine("\n✓ Expense deleted successfully!");
            }
            else
            {
                Console.WriteLine("\n✗ Failed to delete expense.");
            }
        }
        
        private async Task GenerateMonthlyReportAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ MONTHLY REPORT ═══\n");
            
            Console.Write("Enter year (YYYY): ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year.");
                return;
            }
            
            Console.Write("Enter month (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
            {
                Console.WriteLine("Invalid month.");
                return;
            }
            
            var report = await _expenseService.GenerateMonthlyReportAsync(year, month);
            
            Console.WriteLine($"\n╔═══ MONTHLY REPORT FOR {report.MonthName.ToUpper()} ═══╗");
            Console.WriteLine($"║ Total Expenses: ${report.TotalExpenses,-25:F2} ║");
            Console.WriteLine($"║ Total Transactions: {report.TotalTransactions,-21} ║");
            Console.WriteLine($"║ Average Transaction: ${report.AverageExpenseAmount,-20:F2} ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            
            if (report.ExpensesByCategory.Any())
            {
                Console.WriteLine("\n--- EXPENSES BY CATEGORY ---");
                foreach (var category in report.ExpensesByCategory.OrderByDescending(x => x.Value))
                {
                    var percentage = report.TotalExpenses > 0 ? (category.Value / report.TotalExpenses) * 100 : 0;
                    Console.WriteLine($"{category.Key,-15}: ${category.Value,8:F2} ({percentage,5:F1}%)");
                }
            }
            
            if (report.ExpensesByPaymentMethod.Any())
            {
                Console.WriteLine("\n--- EXPENSES BY PAYMENT METHOD ---");
                foreach (var payment in report.ExpensesByPaymentMethod.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"{payment.Key,-15}: ${payment.Value,8:F2}");
                }
            }
            
            if (report.TopExpenses.Any())
            {
                Console.WriteLine("\n--- TOP 5 EXPENSES ---");
                for (int i = 0; i < Math.Min(5, report.TopExpenses.Count); i++)
                {
                    var expense = report.TopExpenses[i];
                    Console.WriteLine($"{i + 1}. {expense.Description,-25} ${expense.Amount,8:F2} ({expense.Date:MM/dd})");
                }
            }
        }
        
        private async Task ShowSummaryAsync()
        {
            Console.Clear();
            Console.WriteLine("═══ EXPENSE SUMMARY ═══\n");
            
            var totalExpenses = await _expenseService.GetTotalExpensesAsync();
            var currentMonth = DateTime.Now;
            var currentMonthExpenses = await _expenseService.GetTotalExpensesByMonthAsync(currentMonth.Year, currentMonth.Month);
            
            Console.WriteLine($"Total All-Time Expenses: ${totalExpenses:F2}");
            Console.WriteLine($"Current Month ({currentMonth:MMMM yyyy}): ${currentMonthExpenses:F2}");
            
            // Show recent expenses
            var recentExpenses = (await _expenseService.GetAllExpensesAsync()).Take(5).ToList();
            if (recentExpenses.Any())
            {
                Console.WriteLine("\n--- RECENT EXPENSES ---");
                foreach (var expense in recentExpenses)
                {
                    Console.WriteLine($"{expense.Date:MM/dd} - {expense.Description,-20} ${expense.Amount,8:F2}");
                }
            }
        }
        
        private ExpenseCategory SelectCategory()
        {
            Console.WriteLine("\nSelect Category:");
            var categories = Enum.GetValues<ExpenseCategory>();
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1,2}. {categories[i]}");
            }
            
            Console.Write("Enter category number: ");
            if (int.TryParse(Console.ReadLine(), out int categoryIndex) && 
                categoryIndex >= 1 && categoryIndex <= categories.Length)
            {
                return categories[categoryIndex - 1];
            }
            
            Console.WriteLine("Invalid selection. Using 'Other' as default.");
            return ExpenseCategory.Other;
        }
        
        private PaymentMethod SelectPaymentMethod()
        {
            Console.WriteLine("\nSelect Payment Method:");
            var methods = Enum.GetValues<PaymentMethod>();
            for (int i = 0; i < methods.Length; i++)
            {
                Console.WriteLine($"{i + 1,2}. {methods[i]}");
            }
            
            Console.Write("Enter payment method number: ");
            if (int.TryParse(Console.ReadLine(), out int methodIndex) && 
                methodIndex >= 1 && methodIndex <= methods.Length)
            {
                return methods[methodIndex - 1];
            }
            
            Console.WriteLine("Invalid selection. Using 'Other' as default.");
            return PaymentMethod.Other;
        }
        
        private string TruncateString(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
                return str ?? "";
                
            return str.Substring(0, maxLength - 3) + "...";
        }
    }
}