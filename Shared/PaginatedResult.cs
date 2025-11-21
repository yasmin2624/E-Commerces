using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageSize ,int pageIndex, int totaleCount ,IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;  
            PageSize = pageSize;
            Count = totaleCount;
            Data = data;


        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

    }
}
