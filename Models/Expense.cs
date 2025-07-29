using System.ComponentModel.DataAnnotations;

namespace MonthlyExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public ExpenseCategory Category { get; set; }
        
        public string? Notes { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} | {Category} | {Description} | ${Amount:F2} | {PaymentMethod}";
        }
    }
}