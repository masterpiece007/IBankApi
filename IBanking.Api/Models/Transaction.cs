using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBanking.Api.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Recipient { get; set; }
        public string Sender { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }

        public virtual AppUser User { get; set; }
    }
}