using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DeliveryDemoService.Models
{
    public class Order
    {
        [Key]
        [Required]
        public long Id { set; get; }
        [Required]
        public OrderStatus Status { set; get; }
        [Required]
        public decimal Value { set; get; }
        public string[] Details { set; get; }
        [Required]
        public string CustomerFullName { set; get; }
        [Required]
        public string ParcelPointId { set; get; }
        public string CustomerPhone { set; get; }

        public void Update(Order order)
        {
            CustomerFullName = order.CustomerFullName;
            CustomerPhone = order.CustomerPhone;
            Value = order.Value;
            Details = order.Details;
        }
    }
}