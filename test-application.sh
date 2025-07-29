#!/bin/bash

echo "=== Monthly Expense Tracker Test ==="
echo ""

# Set PATH for .NET
export PATH="$PATH:/home/ubuntu/.dotnet"

echo "Building the application..."
dotnet build MonthlyExpenseTracker.csproj

if [ $? -eq 0 ]; then
    echo ""
    echo "✓ Build successful!"
    echo ""
    echo "The Monthly Expense Tracker application is ready to run!"
    echo ""
    echo "To start the application, run:"
    echo "  export PATH=\"\$PATH:/home/ubuntu/.dotnet\""
    echo "  dotnet run"
    echo ""
    echo "The application features:"
    echo "  • Add, view, update, and delete expenses"
    echo "  • Categorize expenses (Food, Transportation, Housing, etc.)"
    echo "  • Track payment methods (Cash, Credit Card, etc.)"
    echo "  • Generate detailed monthly reports"
    echo "  • View expenses by month or category"
    echo "  • Persistent JSON storage"
    echo ""
    echo "Sample usage:"
    echo "  1. Add expenses with description, amount, date, category"
    echo "  2. View all expenses or filter by month/category"
    echo "  3. Generate monthly reports with breakdowns and analytics"
    echo "  4. Update or delete existing expenses"
    echo ""
else
    echo "✗ Build failed!"
    exit 1
fi