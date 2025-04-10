using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace PersonalBudgetTracker
{
    public class Transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } 
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public Transaction(string description, decimal amount, string type, string category, DateTime date)
        {
            if (type != "Income" && type != "Expense")
                throw new ArgumentException("Type must be either 'Income' or 'Expense'.");
            Clear();

            Description = description;
            Amount = amount;
            Type = type;
            Category = category;
            Date = date;
        }

        private static void Clear()
        {
            Console.Clear();
        }
    }

    public class BudgetTracker
    {
        private List<Transaction> transactions = new List<Transaction>();

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public decimal GetTotalIncome() =>
            transactions.Where(t => t.Type == "Income" || t.Type == "income").Sum(t => t.Amount);

        public decimal GetTotalExpenses() =>
            transactions.Where(t => t.Type == "Expense" || t.Type == "expense").Sum(t => t.Amount);

        public decimal GetNetSavings() =>
            GetTotalIncome() - GetTotalExpenses();

        public Dictionary<string, decimal> GetCategoryExpenses()
        {
            return transactions
                .Where(t => t.Type == "Expense" || t.Type == "expense")
                .GroupBy(t => t.Category)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
        }

        public List<Transaction> GetSortedTransactions(string sortBy)
        {
            return sortBy.ToLower() switch
            {
                "date" => transactions.OrderBy(t => t.Date).ToList(),
                "category" => transactions.OrderBy(t => t.Category).ToList(),
                "amount" => transactions.OrderByDescending(t => t.Amount).ToList(),
                _ => transactions
            };
        }

        public string GetMostSpentCategory()
        {
            var categoryExpenses = GetCategoryExpenses();
            return categoryExpenses.OrderByDescending(kv => kv.Value).FirstOrDefault().Key ?? "None";
        }

        public void DisplayTextGraph()
        {
            var categoryExpenses = GetCategoryExpenses();
            Console.WriteLine("\nCategory Expense Graph:");
            foreach (var category in categoryExpenses)
            {
                Console.Write($"{category.Key,-15}: ");
                Console.WriteLine(new string('#', (int)(category.Value / 10)));
            }
        }

        public void DisplaySummary()
        {
            Console.Clear();
            Console.WriteLine("\n--- Budget Summary ---");
            Console.WriteLine($"Total Income     : {GetTotalIncome():C}");
            Console.WriteLine($"Total Expenses   : {GetTotalExpenses():C}");
            Console.WriteLine($"Net Savings      : {GetNetSavings():C}");
            Console.WriteLine($"Top Spent Category: {GetMostSpentCategory()}");
        }
    }

    class Program
    {
        static void Main()
        {
            BudgetTracker tracker = new BudgetTracker();
            Console.WriteLine("====== Personal Budget Tracker ======");

            while (true)
            {
                Console.WriteLine("\n1. Add Transaction");
                Console.WriteLine("2. View Summary");
                Console.WriteLine("3. View Category Graph");
                Console.WriteLine("4. View Sorted Transactions");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("===== Transaction =====");
                            Console.Write("Description: ");
                            string desc = Console.ReadLine();

                            Console.Write("Amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());

                            Console.Write("Type (Income/Expense): ");
                            string type = Console.ReadLine();

                            Console.Write("Category: ");
                            string category = Console.ReadLine();

                            Console.Write("Date (yyyy-mm-dd): ");
                            DateTime date = DateTime.Parse(Console.ReadLine());

                            Transaction t = new Transaction(desc, amount, type, category, date);
                            tracker.AddTransaction(t);
                            Console.WriteLine("Transaction added successfully.");
                            break;

                        case "2":
                            tracker.DisplaySummary();
                            break;

                        case "3":
                            tracker.DisplayTextGraph();
                            break;

                        case "4":
                            Console.Clear();
                            Console.WriteLine("===== View Sorted Transactions =====");
                            Console.Write("Sort by (date/category/amount): ");
                            string sortBy = Console.ReadLine();
                            var sorted = tracker.GetSortedTransactions(sortBy);

                            Console.WriteLine("\n--- Sorted Transactions ---");
                            foreach (var trans in sorted)
                            {
                                Console.WriteLine($"{trans.Date.ToShortDateString()} | {trans.Type,-7} | {trans.Category,-12} | {trans.Amount,8:C} | {trans.Description}");
                            }
                            break;

                        case "5":
                            Console.WriteLine("Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
