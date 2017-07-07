using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using StateStreet.Models;

namespace StateStreet.ConsoleApp
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static AccountModel model;
        private static AccountModelService accountModelService;
        static void Main(string[] args)
        {
            logger.Info("Application Started");
            accountModelService = new AccountModelService();
            //accountModelService.AddAccounts();
            Console.WriteLine("===============================");
            Console.WriteLine("Welcome to State Street");
            Console.WriteLine("===============================");
            Login();
        }

        public static void BankServices()
        {
            Console.WriteLine("Press 1 TO Check Balance in your account");
            Console.WriteLine("Press 2 to withdraw money from your account");
            Console.WriteLine("Press 3 to Get your transaction list");
            Console.WriteLine("Press 4 to Logout");
            Console.WriteLine("Press 5 to Logout and Exit");
            var key = Console.ReadLine();
            SelectOptions(key);
        }

        public static void Login()
        {
            while (true)
            {
                Console.WriteLine("Please login to continue with our services");
                Console.WriteLine("Click Enter to login");
                var key = Console.ReadLine();
                if (string.IsNullOrEmpty(key))
                {
                    model = accountModelService.Login();
                    if (model?.AccountNumber != null)
                    {
                        logger.Info("Successful login for the user " + model.FirstName + " " + model.LastName);
                        BankServices();
                    }
                    else
                    {
                        continue;
                    }
                }
                break;
            }
        }

        private static void Format()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("=======================================");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void SelectOptions(string option)
        {
            Console.Clear();
            switch (option)
            {
                case "1":
                    logger.Info("Performing Check Balance Operation");
                    if (model != null)
                    {
                        Console.WriteLine("Balance in your account is " + model.Balance + " dollars");
                        Format();
                        BankServices();
                    }
                    else
                    {
                        Login();
                    }
                    break;
                case "2":
                    logger.Info("Performing Withdraw Operation");
                    if (model != null)
                    {
                        Console.WriteLine("Enter the amount you want to withdraw");
                        var amount = Console.ReadLine();
                        model = accountModelService.WithDrawAmount(model, amount);
                        Format();
                        BankServices();
                    }
                    else
                    {
                        Login();
                    }
                    break;
                case "3":
                    if (model != null)
                    {
                        logger.Info("Performing Get transactions Operation");
                        accountModelService.GetAllTransactions(model.AccountNumber);
                        Format();
                        BankServices();
                    }
                    else
                    {
                        Login();
                    }
                    break;
                case "4":
                    model = null;                    
                    logger.Info("Performing Logout Operation");
                    Console.WriteLine("You are successfully logged out of the application");
                    Format();
                    Login();
                    break;
                case "5":
                    model = null;
                    Console.WriteLine("You are successfully logged out of the application");
                    Console.WriteLine("Closing the application now");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("That is not a valid entry");
                    BankServices();
                    break;
            }
        }
    }
}
