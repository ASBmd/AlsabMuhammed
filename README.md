# Monthly Expense Tracker

A comprehensive console-based expense tracking application built with C# and .NET 8. This application helps you manage your personal finances by tracking expenses, categorizing them, and generating detailed monthly reports.

## Features

### 📊 Expense Management
- **Add Expenses**: Record new expenses with detailed information
- **View Expenses**: Display all expenses or filter by month/category
- **Update Expenses**: Modify existing expense records
- **Delete Expenses**: Remove unwanted expense entries
- **Recurring Expenses**: Mark expenses as recurring for better tracking

### 📈 Reporting & Analytics
- **Monthly Reports**: Generate comprehensive monthly expense reports
- **Category Analysis**: View expenses grouped by categories with percentages
- **Payment Method Tracking**: Track expenses by payment methods
- **Top Expenses**: Identify your highest expenses
- **Summary Dashboard**: Quick overview of total and current month expenses

### 💾 Data Management
- **JSON Storage**: All data is stored in a local JSON file
- **Data Persistence**: Expenses are automatically saved and loaded
- **Backup Support**: Automatic backup functionality

### 🎨 User Interface
- **Interactive Console Menu**: Easy-to-navigate menu system
- **Formatted Output**: Clean, organized display of data
- **Input Validation**: Robust error handling and data validation

## Expense Categories

The application supports the following expense categories:
- Food
- Transportation
- Housing
- Utilities
- Healthcare
- Entertainment
- Shopping
- Education
- Insurance
- Savings
- Debt
- Personal
- Travel
- Business
- Other

## Payment Methods

Track expenses by payment method:
- Cash
- Credit Card
- Debit Card
- Bank Transfer
- Check
- Digital Wallet
- Other

## Installation & Setup

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

### Installation Steps

1. **Clone or download the project files**
2. **Navigate to the project directory**
   ```bash
   cd MonthlyExpenseTracker
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

## Usage Guide

### Starting the Application
When you run the application, you'll see the main menu with the following options:

```
┌─────────────────────────────────────────────────────────────┐
│                        MAIN MENU                           │
├─────────────────────────────────────────────────────────────┤
│ 1. Add New Expense                                         │
│ 2. View All Expenses                                       │
│ 3. View Expenses by Month                                  │
│ 4. View Expenses by Category                               │
│ 5. Update Expense                                          │
│ 6. Delete Expense                                          │
│ 7. Generate Monthly Report                                 │
│ 8. Show Summary                                            │
│ 9. Exit                                                    │
└─────────────────────────────────────────────────────────────┘
```

### Adding an Expense
1. Select option 1 from the main menu
2. Enter the expense description
3. Enter the amount (must be positive)
4. Enter the date (YYYY-MM-DD format) or press Enter for today
5. Select a category from the numbered list
6. Select a payment method from the numbered list
7. Add optional notes
8. Specify if it's a recurring expense

### Viewing Expenses
- **All Expenses**: Shows all expenses sorted by date (newest first)
- **By Month**: Filter expenses for a specific month and year
- **By Category**: View all expenses for a selected category

### Generating Reports
The monthly report feature provides:
- Total expenses for the month
- Number of transactions
- Average transaction amount
- Expenses breakdown by category (with percentages)
- Expenses breakdown by payment method
- Top 5 highest expenses

### Data Storage
- All expense data is stored in `expenses.json` in the application directory
- The file is automatically created when you add your first expense
- Data is automatically saved after each operation
- Backup files are created with timestamps when needed

## Project Structure

```
MonthlyExpenseTracker/
├── Models/
│   ├── Expense.cs              # Main expense model
│   ├── ExpenseCategory.cs      # Expense categories enum
│   ├── PaymentMethod.cs        # Payment methods enum
│   └── MonthlyReport.cs        # Monthly report model
├── Services/
│   ├── IExpenseService.cs      # Expense service interface
│   ├── ExpenseService.cs       # Main expense service implementation
│   └── JsonDataService.cs     # JSON data persistence service
├── UI/
│   └── ConsoleUI.cs           # Console user interface
├── Program.cs                  # Application entry point
├── MonthlyExpenseTracker.csproj # Project file
└── README.md                   # This file
```

## Technical Details

### Dependencies
- **Newtonsoft.Json**: For JSON serialization and deserialization
- **System.ComponentModel.DataAnnotations**: For model validation

### Architecture
- **Models**: Data models representing expenses and reports
- **Services**: Business logic and data access layer
- **UI**: User interface layer with console-based interaction
- **Separation of Concerns**: Clear separation between data, business logic, and presentation

### Error Handling
- Input validation for all user inputs
- Graceful error handling with user-friendly messages
- Try-catch blocks around critical operations
- File I/O error handling

## Sample Usage

### Adding Sample Expenses
```
Description: Grocery Shopping
Amount ($): 85.50
Date: 2024-01-15
Category: Food
Payment Method: Credit Card
Notes: Weekly groceries
Recurring: No
```

### Sample Monthly Report Output
```
╔═══ MONTHLY REPORT FOR JANUARY 2024 ═══╗
║ Total Expenses: $1,247.83              ║
║ Total Transactions: 23                 ║
║ Average Transaction: $54.25            ║
╚════════════════════════════════════════╝

--- EXPENSES BY CATEGORY ---
Food           :  $423.50 ( 34.0%)
Transportation :  $245.00 ( 19.6%)
Housing        :  $350.00 ( 28.1%)
Entertainment  :  $129.33 ( 10.4%)
Other          :   $99.00 (  7.9%)

--- TOP 5 EXPENSES ---
1. Rent Payment            $  350.00 (01/01)
2. Car Insurance           $  125.00 (01/15)
3. Grocery Shopping        $   85.50 (01/15)
4. Gas Station             $   65.00 (01/10)
5. Movie Theater           $   45.00 (01/20)
```

## Contributing

Feel free to contribute to this project by:
1. Reporting bugs
2. Suggesting new features
3. Submitting pull requests
4. Improving documentation

## License

This project is open source and available under the MIT License.

## Support

If you encounter any issues or have questions about using the Monthly Expense Tracker, please check the documentation or create an issue in the project repository.

---

**Happy expense tracking! 💰📊**