using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IBanking.Api.Models;
using IBanking.Api.Persistence;
using System.Web.Http.Cors;

namespace IBanking.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TransactionController : ApiController
    {
        public ITransactionRepository transactionRepository { get; set; }
        public IBankAccountRepository bankRepository { get; set; }

        public TransactionController()
        {
            transactionRepository = new TransactionRepository();
            bankRepository = new BankAccountRepository();
        }
        [HttpPost]
        public async Task<IHttpActionResult> SendMoney(Transaction transaction) {
            if (ModelState.IsValid)
            {
               
                var isCreated = await transactionRepository.CreateTransaction(transaction);
                if (isCreated)
                {
                    return Ok(transaction);
                }
                return BadRequest("unable to send money at this moment");
            }
            return BadRequest("modelstate is invalid");
        }
        [HttpGet]
        public HttpResponseMessage GetData(string username) {
            try
            {
                var user = bankRepository.GetUser(username);
                var account = bankRepository.GetAccount(user.Id);
                var txns = transactionRepository.GetTransactions(username);
                var obj = new
                {
                    account,
                    txns
                };
                try
                {
                    if (account != null)
                    {
                        var serialized = JsonConvert.SerializeObject(obj);
                        return Request.CreateResponse(HttpStatusCode.OK, obj);

                    }
                }
                catch (Exception ee)
                {

                    throw;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"errr");

            }
            catch (Exception e)
            {

                throw;
            }
               
           }
        [HttpGet]
        public IHttpActionResult GetRecentTransactions(string userId) {
            if (!string.IsNullOrEmpty(userId))
            {
               var recentTransactions = transactionRepository.GetRecentTransactions(userId);
                if (recentTransactions != null)
                {
                    return Ok(recentTransactions);
                }
                return BadRequest("no recent tansactions for this user");
            }
            return BadRequest("userid is invalid");
        }
        [HttpGet]
        public IHttpActionResult GetAllTransactions(string userId) {
            if (!string.IsNullOrEmpty(userId))
            {
                var recentTransactions = transactionRepository.GetTransactions(userId);
                if (recentTransactions != null)
                {
                    return Ok(recentTransactions);
                }
                return BadRequest("no recent tansactions for this user");
            }
            return BadRequest("userid is invalid");
        }
    }
}
