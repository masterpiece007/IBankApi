using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBanking.Api.Models;

namespace IBanking.Api.Persistence
{
    public interface IBankAccountRepository
    {
        Task<bool> CreateAccount(BankAccount bankAccount);
        AppUser GetUser(string username);
        BankAccount GetAccount(string id);
    }
}
