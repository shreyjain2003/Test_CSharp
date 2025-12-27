using System;

namespace Test_CSharp.QuickMart_ProfitCalculator
{
    /// <summary>
    /// Stores details of a sales transaction and its profit or loss result.
    /// </summary>
    public class SaleTransaction
    {
        /// <summary>Invoice number of the transaction.</summary>
        public string InvoiceNo { get; set; }

        /// <summary>Name of the customer.</summary>
        public string CustomerName { get; set; }

        /// <summary>Name of the item sold.</summary>
        public string ItemName { get; set; }

        /// <summary>Quantity of items sold.</summary>
        public int Quantity { get; set; }

        /// <summary>Total purchase amount.</summary>
        public decimal PurchaseAmount { get; set; }

        /// <summary>Total selling amount.</summary>
        public decimal SellingAmount { get; set; }

        /// <summary>Indicates PROFIT, LOSS, or BREAK-EVEN.</summary>
        public string ProfitOrLossStatus { get; set; }

        /// <summary>Calculated profit or loss amount.</summary>
        public decimal ProfitOrLossAmount { get; set; }

        /// <summary>Profit or loss percentage.</summary>
        public decimal ProfitMarginPercent { get; set; }
    }

    /// <summary>
    /// Entry point class for QuickMart Profit Calculator console application.
    /// </summary>
    class Program2
    {
        /// <summary>Stores the most recent transaction.</summary>
        static SaleTransaction LastTransaction = null;

        /// <summary>Indicates whether a transaction exists.</summary>
        static bool HasLastTransaction = false;

        /// <summary>
        /// Application entry point and menu handler.
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateNewTransaction();
                        break;
                    case "2":
                        ViewLastTransaction();
                        break;
                    case "3":
                        CalculateProfitLoss();
                        break;
                    case "4":
                        Console.WriteLine("Application closed.");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }


        /// <summary>
        /// Creates a new sales transaction and computes profit or loss.
        /// </summary>
        #region CreateNewTransaction
        public static void CreateNewTransaction()
        {
            SaleTransaction pl = new SaleTransaction();

            Console.Write("Enter Invoice No: ");
            pl.InvoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pl.InvoiceNo)) return;

            Console.Write("Enter Customer Name: ");
            pl.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            pl.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            pl.Quantity = Convert.ToInt32(Console.ReadLine());
            if (pl.Quantity <= 0) return;

            Console.Write("Enter Purchase Amount: ");
            pl.PurchaseAmount = Convert.ToDecimal(Console.ReadLine());
            if (pl.PurchaseAmount <= 0) return;

            Console.Write("Enter Selling Amount: ");
            pl.SellingAmount = Convert.ToDecimal(Console.ReadLine());
            if (pl.SellingAmount < 0) return;

            ComputeProfitLoss(pl);

            LastTransaction = pl;
            HasLastTransaction = true;

            Console.WriteLine("Transaction saved successfully.");
        }
        #endregion


        /// <summary>
        /// Displays the most recent transaction details.
        /// </summary>
        #region ViewLastTransaction
        public static void ViewLastTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available.");
                return;
            }

            Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}");
            Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
            Console.WriteLine($"Item: {LastTransaction.ItemName}");
            Console.WriteLine($"Final Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss: {LastTransaction.ProfitOrLossAmount:F2}");
        }
        #endregion 


        /// <summary>
        /// Recalculates and displays profit or loss for the last transaction.
        /// </summary>
        #region CalculateProfitLoss
        public static void CalculateProfitLoss()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available.");
                return;
            }

            ComputeProfitLoss(LastTransaction);

            Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
        }
        #endregion


        /// <summary>
        /// Computes profit or loss and margin for a transaction.
        /// </summary>
        private static void ComputeProfitLoss(SaleTransaction pl)
        {
            if (pl.SellingAmount > pl.PurchaseAmount)
            {
                pl.ProfitOrLossStatus = "PROFIT";
                pl.ProfitOrLossAmount = pl.SellingAmount - pl.PurchaseAmount;
            }
            else if (pl.SellingAmount < pl.PurchaseAmount)
            {
                pl.ProfitOrLossStatus = "LOSS";
                pl.ProfitOrLossAmount = pl.PurchaseAmount - pl.SellingAmount;
            }
            else
            {
                pl.ProfitOrLossStatus = "BREAK-EVEN";
                pl.ProfitOrLossAmount = 0;
            }

            pl.ProfitMarginPercent =
                (pl.ProfitOrLossAmount / pl.PurchaseAmount) * 100;
        }
    }
}
