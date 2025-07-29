namespace MonthlyExpenseTracker.Models
{
    public class MonthlyReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalExpenses { get; set; }
        public Dictionary<ExpenseCategory, decimal> ExpensesByCategory { get; set; } = new();
        public Dictionary<PaymentMethod, decimal> ExpensesByPaymentMethod { get; set; } = new();
        public List<Expense> TopExpenses { get; set; } = new();
        public int TotalTransactions { get; set; }
        public decimal AverageExpenseAmount { get; set; }
        
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
        
        public override string ToString()
        {
            return $"Monthly Report for {MonthName}\n" +
                   $"Total Expenses: ${TotalExpenses:F2}\n" +
                   $"Total Transactions: {TotalTransactions}\n" +
                   $"Average Transaction: ${AverageExpenseAmount:F2}";
        }
    }
}