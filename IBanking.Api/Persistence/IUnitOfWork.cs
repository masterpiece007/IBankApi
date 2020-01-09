using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBanking.Api.Persistence
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
