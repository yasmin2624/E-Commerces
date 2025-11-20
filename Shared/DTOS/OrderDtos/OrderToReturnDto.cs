using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public ShippingAddressDto Address { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItemsDto> Items { get; set; } = [];
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }


    }
}
