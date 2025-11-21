using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOS.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.controllers
{
    public class OrdersController(IServiceManager serviceManager) : APIBaseController
    {
        #region Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            //var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.OrderService.CreateOrder(orderDto,GetEmailFromToken());
            return Ok(Order);
        }

        #endregion
       
        #region GetDeliveryMethods
        [Authorize]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await serviceManager.OrderService.GetAllDeliveryMethods();
            return Ok(DeliveryMethods);
        }
        #endregion

        #region  GetAllOrdersByEmail
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Order = await serviceManager.OrderService.GetAllOrderAsync(GetEmailFromToken());
            return Ok(Order);
        }
        #endregion

        #region  Get Order By Id
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var Order = await serviceManager.OrderService.GetOrderByIDAsync(id);
            return Ok(Order);
        }
        #endregion



    }
}