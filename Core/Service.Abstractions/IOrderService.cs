using Shared.DTOS.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email);
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string Email);
        Task<OrderToReturnDto> GetOrderByIDAsync( Guid Id);
    }
}
