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
    public class BankAccountRepository : IBankAccountRepository
    {
        private ApplicationDbContext context { get; set; }
        private IUnitOfWork unitOfWork { get; set; }
        //private UserManager<AppUser> UserManager { get; set; }


        public BankAccountRepository()
        {
            context = new ApplicationDbContext();
            unitOfWork = new UnitOfWork(context);
            //UserManager = new UserManager<AppUser>(new UserStore<AppUser>(context));

        }
        public async Task<bool> CreateAccount(BankAccount bankAccount)
        {
            if (bankAccount != null)
            {
                //var id =  HttpContext.Current.User.Identity.GetUserId();
                //var user = context.Users.FirstOrDefault(a => a.Id == id);
                
                //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //var userAccount = context.BankAccounts.FirstOrDefault(a => a.UserId == transaction.User.Id);
                var userAccount = context.BankAccounts.FirstOrDefault(a => a.BankAccountId == bankAccount.BankAccountId);
                if (userAccount == null)
                {                  
                    context.BankAccounts.Add(bankAccount);
                    await unitOfWork.Complete();
                    return true;
                }
                return false;
            }
            return false;
        }
       public AppUser GetUser(string username)
        {
            return context.Users.FirstOrDefault(a => a.UserName == username);
        }
        public BankAccount GetAccount(string id)
        {
            return context.BankAccounts.FirstOrDefault(a => a.BankAccountId == id);
        }
    }
}