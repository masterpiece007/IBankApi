using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IBanking.Api.Models;

namespace IBanking.Api.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext dbContext { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public async Task Complete()
        {
          int saved = await dbContext.SaveChangesAsync();
        }
    }
}