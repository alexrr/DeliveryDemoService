using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DeliveryDemoService.Data;
using DeliveryDemoService.Models;

namespace DeliveryDemoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelPointController : ControllerBase
    {
        private readonly IDeliveryDemoRepo deliveryRepo;

        public ParcelPointController(IDeliveryDemoRepo repo)
        {
            deliveryRepo = repo;
        }

        // GET: api/<ParcelPointController>
        [HttpGet]
        public IEnumerable<ParcelPoint> Get()
        {
            return deliveryRepo.GetAllParcelPoints(point => point.Id);
        }

        // GET api/<ParcelPointController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var point = deliveryRepo.GetParcelPoint(id);
            if (point == null)
                return NotFound();
            return Ok(point);
        }
    }
}