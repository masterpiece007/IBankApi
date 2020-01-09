using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBanking.Api.Models
{
    public class BankAccount
    {
        public BankAccount()
        {
            Transactions = new Collection<Transaction>();
        }
        [Key, ForeignKey("AppUser")]
        public string BankAccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime LastUpdated { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
