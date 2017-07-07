using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateStreet.Entities;
using StateStreet.Models;

namespace StateStreet.Services.Services
{
    public class AccountService
    {
        private AccountModelContext context;
        public AccountService()
        {
            context = new AccountModelContext();
        }
        public int CreateAccount(AccountModel model)
        {            
            var account = new Account()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                AccountNumber = "SS" + GetAccountNumber(),
                Password = GetPasswordHash(model.Password),
                Balance = model.Balance
            };
            context.Accounts.Add(account);
            int result = context.SaveChanges();
            return result;
        }

        private int GetAccountNumber()
        {
            Random random = new Random();
            return random.Next(111, 999999);
        }

        private string GetPasswordHash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public AccountModel Login(string email, string password)
        {
            password = GetPasswordHash(password);
            var user = context.Accounts.SingleOrDefault(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && x.Password == password);
            if (user != null)
            {
                return new AccountModel()
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    AccountNumber = user.AccountNumber,
                    Balance = user.Balance,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    Password = user.Password
                };
            }

            return null;

        }

        public AccountModel WithDrawAmount(AccountModel model, decimal amount)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = context.Accounts.SingleOrDefault(x => x.Id == model.Id);
                    user.Balance = user.Balance - amount;
                    var transacion = new AccountTransaction
                    {
                        AccountNumber = model.AccountNumber,
                        Amount = amount,
                        TransactionType = "WithDraw",
                        TransactionTime = DateTime.Now
                    };
                    context.AccountTransactions.Add(transacion);
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                    return new AccountModel()
                    {

                        Email = user.Email,
                        LastName = user.LastName,
                        AccountNumber = user.AccountNumber,
                        Balance = user.Balance,
                        FirstName = user.FirstName,
                        Id = user.Id,
                        Password = user.Password
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        public List<AccountTransactionModel> GetAllTransactions(string accountNumber)
        {
            var query = (from transaction in context.AccountTransactions
                where transaction.AccountNumber == accountNumber
                select new AccountTransactionModel()
                {
                    Id = transaction.Id,
                    AccountNumber = transaction.AccountNumber,
                    TransactionTime = transaction.TransactionTime,
                    TransactionType = transaction.TransactionType,
                    Amount = transaction.Amount
                }).ToList();
            return query;
        }
    }
}