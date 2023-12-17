using AutoMapper;
using FluentValidation.Results;
using Orders.Application;
using Orders.Domain;
using Orders.Dtos;
using Orders.Infrastructure;
using Orders.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Application
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly string tableName = "Orders";
       
        public OrderService(OrderRepository orderRepository, IMapper mapper, CreateOrderDtoValidator createOrderValidator, UpdateOrderDtoValidator updateOrderDtoValidator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto orderDto)
        {
            //CreateOrderDto orderDto = _mapper.Map<CreateOrderDto>(order);
            return await _orderRepository.CreateAsync(orderDto, tableName);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id, tableName);
        }

        public async Task<IEnumerable<object>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync(tableName);
        }

        public async Task<object> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id, tableName);
        }

        public async Task<bool> UpdateOrderAsync(UpdateOrderDto order) // order is partof orders not all should sent always
        {
            return await _orderRepository.UpdateAsync(order, tableName);
        }


    }
}
