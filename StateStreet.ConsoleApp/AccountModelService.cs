using System;
using NLog;
using StateStreet.Models;
using StateStreet.Services.Services;

namespace StateStreet.ConsoleApp
{
    public class AccountModelService
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly AccountService accountService;
        public AccountModelService()
        {
            accountService = new AccountService();
        }

        public void AddAccounts()
        {
            var account = new AccountModel()
            {
                Email = "alex@gmail.com",
                FirstName = "Alex",
                LastName = "Thompson",
                Password = "09011U0504",
                Balance = 20000
            };

            var account2 = new AccountModel()
            {
                Email = "jane@gmail.com",
                FirstName = "Jane",
                LastName = "Ryder",
                Password = "09011U0512",
                Balance = 20000
            };

            accountService.CreateAccount(account);
            accountService.CreateAccount(account2);
        }

        public AccountModel Login()
        {
            Console.WriteLine("Please Enter your Email Id and Press Enter");
            string email = Console.ReadLine();
            Console.WriteLine("Please Enter your Password and Press Enter");
            string password = ReadPassword();
            //string password = Console.ReadLine();
            var result = accountService.Login(email, password);
            if (result != null)
            {
                Console.WriteLine("Hi there !! Welcome " + result.FirstName + " " + result.LastName);
            }
            else
            {
                Console.WriteLine("Email Id and Password do not match. Please try again");
            }

            return result;
        }

        public AccountModel WithDrawAmount(AccountModel model, string amount)
        {
            decimal deductableAmount = 0;
            Decimal.TryParse(amount, out deductableAmount);
            if (deductableAmount != 0 && deductableAmount < model.Balance)
            {
                var result = accountService.WithDrawAmount(model, deductableAmount);
                logger.Info("Amount withdrawn successfully");
                Console.WriteLine("Transaction Successful. Current Balance in your account is " + result.Balance);
                return result;
            }
            else
            {
                Console.WriteLine("Press enter a number between 1 and " + model.Balance);
                return model;
            }
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }            
            Console.WriteLine();
            return password;
        }
    }
}