using Domain.Contracts;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CashService(ICashRepository cashRepository) : ICashService
    {
        public async Task<string?> GetAsync(string cashKey)
            => await cashRepository.GetAsync(cashKey);

        public async Task SetAsync(string cashKey, object cashValue, TimeSpan timeToLive)
        {
            var Value = JsonSerializer.Serialize(cashValue);
            await cashRepository.SetAsync(cashKey, Value, timeToLive);
        }
    }
}
