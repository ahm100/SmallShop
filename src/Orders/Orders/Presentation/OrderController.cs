using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orders.Application;
using Orders.Domain;
using Orders.Dtos;
using Orders.Validators;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly CreateOrderDtoValidator _createOrderValidator;
        private readonly UpdateOrderDtoValidator _updateOrderValidator;
        public OrderController(IOrderService orderService, UpdateOrderDtoValidator updateOrderDtoValidator, CreateOrderDtoValidator createOrderValidator)
        {
            _orderService = orderService;
            _createOrderValidator = createOrderValidator;
            _updateOrderValidator = updateOrderDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto order)
        {
            //throw new Exception("Simulated internal server error");
            ValidationResult validationResult = _createOrderValidator.Validate(order);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    Log.Error("Validation Error: " +error.ErrorMessage); // Log the error message
                }
                return BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            var result = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = result }, order);
        }

        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto order) // object might be some properties of order entity
        {
            if (order == null) return BadRequest();
            ValidationResult validationResult = _updateOrderValidator.Validate(order);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    Log.Error("Validation Error: " + error.ErrorMessage); // Log the error message
                }
                return BadRequest(new { Errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            //var idProperty = order.GetType().GetProperty("Id");
            //var idValue = idProperty.GetValue(order);

            var existingOrder = await _orderService.GetOrderByIdAsync(order.Id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
