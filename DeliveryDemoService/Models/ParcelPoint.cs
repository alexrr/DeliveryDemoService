using System.ComponentModel.DataAnnotations;

namespace DeliveryDemoService.Models
{
    public class ParcelPoint
    {
        [Key]
        [Required]
        public string Id { set; get; }
        public string Address { set; get; }
        [Required]
        public bool Status { set; get; }
    }
}