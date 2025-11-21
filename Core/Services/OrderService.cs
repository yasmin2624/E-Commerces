using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderModules;
using Domain.Entities.ProductModules;
using Domain.Exceptions;
using Service.Abstractions;
using Service.Specifications;
using Shared.DTOS.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IBasketRepository basketRepository, IMapper mapper ,IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email)
        {
            // Map Address To OrderAddress
            var OrderAddress = mapper.Map<ShippingAddressDto,ShippingAddress>(orderDto.ShipToAddress);

            // Get Basket 
            var Basket = await basketRepository.GetBasketAsync(orderDto.BasketId)
                ??throw new BasketNotFoundException(orderDto.BasketId);
            //Create OrderItems List 
            List<OrderItem> orderItems = [];
            var ProductRepo = unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrderd
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Price = product.Price,
                    Quantity = item.Quantity
                };

                orderItems.Add(orderItem);
            }
            //Get DeliveryMethod
            var DeliveryMethod =await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ??throw new DelivaryMethodNotFoundException(orderDto.DeliveryMethodId);
            //Calculate Subtotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            //Create Order
            var order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, subtotal);
            //Add Order 
            await unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<Order, OrderToReturnDto>(order);


        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            var DeliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string Email)
        {
            var spec = new OrderSpecifications(Email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDto>>(orders);



        }

        public async Task<OrderToReturnDto> GetOrderByIDAsync(Guid Id)
        {
            var Spec = new OrderSpecifications(Id);
            var Order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return mapper.Map<OrderToReturnDto>(Order);
        }
    }
}
