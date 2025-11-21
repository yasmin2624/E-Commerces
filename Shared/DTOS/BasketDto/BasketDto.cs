using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDto
{
    public class BasketDto
    {
        public string Id { get; set; } 
        public ICollection<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingPrice { get; set; }//DeliveryMethod.Price
        public int? DeliveryMethodId { get; set; }
    }
}
