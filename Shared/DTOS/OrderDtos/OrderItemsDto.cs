using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDtos
{
    public class OrderItemsDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
