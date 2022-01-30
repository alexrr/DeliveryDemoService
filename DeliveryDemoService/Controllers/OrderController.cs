using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DeliveryDemoService.Data;
using DeliveryDemoService.Models;

namespace DeliveryDemoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IDeliveryDemoRepo deliveryRepo;

        public OrderController(IDeliveryDemoRepo repo)
        {
            deliveryRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return deliveryRepo.GetAllOrders();
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = deliveryRepo?.GetOrderId(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            return deliveryRepo.CreateOrder(order) switch
            {
                CreateOrderResult.Success => Ok(order),
                CreateOrderResult.Error => BadRequest(),
                CreateOrderResult.Forbidden => StatusCode(403),
                _ => BadRequest()
            };
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            return deliveryRepo.UpdateOrder(id, ref order) switch
            {
                UpdateOrderResult.Success => Ok(order),
                UpdateOrderResult.Error => BadRequest(),
                UpdateOrderResult.NotFound => StatusCode(403),
                _ => BadRequest()
            };
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            deliveryRepo.CancelOrder(id);
        }
    }
}