using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICashRepository
    {
        //Get 
        Task <string?>GetAsync(string Cashkey);

        //Set
        Task SetAsync(string Cashkey,string Cashvalue,TimeSpan TimeToLive);


    }
}
