using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class CashRepository(IConnectionMultiplexer connection) : ICashRepository
    {
        readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string Cashkey)
        {
            var CashValue =await _database.StringGetAsync(Cashkey);
            return CashValue.IsNullOrEmpty ? null : CashValue.ToString();
        }


        public async Task SetAsync(string Cashkey, string Cashvalue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(Cashkey, Cashvalue, TimeToLive);
        }
    }
}
