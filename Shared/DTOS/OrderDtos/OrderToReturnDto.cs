using Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDtos
{
    public record OrderToReturnDto
    {
        public Guid Id { get; init; }
        public string buyerEmail { get; init; } = string.Empty;
        public AddressDto shipToAddress { get; init; }
        public ICollection<OrderItemsDto> items { get; init; } = new List<OrderItemsDto>();
        public string status { get; init; } = string.Empty;
        public string deliveryMethod { get; init; } = string.Empty;
        public int? DeliveryMethodId { get; init; }
        public decimal deliveryCost { get; set; }
        public decimal subtotal { get; init; } //OrderItem1.Q * OrderItem1.Price + OrderItem2.Q * OrderItem2.Price
        public DateTimeOffset orderDate { get; init; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; init; } = string.Empty;
        public decimal Total { get; init; }


    }
}
