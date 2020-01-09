using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IBanking.Api.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IBanking.Api.Persistence
{
    public class TransactionRepository : ITransactionRepository
    {
        private ApplicationDbContext context { get; set; }
        private IUnitOfWork unitOfWork { get; set; }
        private UserManager<AppUser> UserManager { get; set; }


        public TransactionRepository()
        {
            context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
            UserManager = new UserManager<AppUser>(new UserStore<AppUser>(context));

        }
        public async Task<bool> CreateTransaction(Transaction transaction)
        {
            if (transaction != null)
            {
                var id =  HttpContext.Current.User.Identity.GetUserId();
                var user = context.Users.FirstOrDefault(a => a.UserName == transaction.Sender);
                
                //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //var userAccount = context.BankAccounts.FirstOrDefault(a => a.UserId == transaction.User.Id);
                var userAccount = context.BankAccounts.FirstOrDefault(a => a.BankAccountId == user.Id);
                if (userAccount != null)
                {
                    if (transaction.Amount > userAccount.AccountBalance)
                    {
                        return false;
                    }
                    userAccount.AccountBalance = userAccount.AccountBalance - transaction.Amount;
                    context.Transactions.Add(transaction);
                    await unitOfWork.Complete();
                    return true;
                }
                return false;
            }
            return false;
        }
        public List<Transaction> GetRecentTransactions(string userId)
        {
            if (userId != null)
            {
                var transactions = context.Transactions.Include("Users")
                    .Where(a => a.User.Id == userId)
                    .OrderByDescending(a => a.TransactionDate)
                    .Take(5).ToList();
                if (transactions.Count() != 0)
                {
                    return transactions;
                }
                return null;
            }
            return null;
        }
        public List<Transaction> GetTransactions(string sender)
        {
            if (sender != null)
            {
                var transactions = context.Transactions
                    .Where(a => a.Sender == sender)
                    .OrderByDescending(a => a.TransactionDate)
                    .ToList();
                if (transactions.Count() != 0)
                {
                    return transactions;
                }
                return null;
            }
            return null;
        }
       
    }
}