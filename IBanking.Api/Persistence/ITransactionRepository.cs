using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBanking.Api.Models;

namespace IBanking.Api.Persistence
{
    public interface ITransactionRepository
    {
        Task<bool> CreateTransaction(Transaction transaction);
        List<Transaction> GetRecentTransactions(string userId);
        List<Transaction> GetTransactions(string userId);

    }
}
