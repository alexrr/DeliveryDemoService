using System;
using System.Linq;
using System.Text.RegularExpressions;
using DeliveryDemoService.Models;

namespace DeliveryDemoService.Data
{
    public class OrderValidator : IValidator<Order>
    {
        private readonly AppDbContext _context;
        public OrderValidator(AppDbContext context)
        {
            _context = context;
        }

        private readonly Regex phoneFormat = new(@"^\+7\d{3}-\d{3}-\d{2}-\d{2}$");
        private readonly Regex pointIdFormat = new(@"^\d{4}-\d{3}$");

        public ValidateResult Validate(Order order)
        {
            if (order == null)
            {
                return ValidateResult.NotValid;
            }
            var point = _context.ParcelPoints.FirstOrDefault(parcelPoint =>
                string.Equals(parcelPoint.Id, order.ParcelPointId));
            if (point == null)
            {
                return ValidateResult.NotValid;
            }
            if (!point.Status)
            {
                return ValidateResult.Forbidden;
            }
            if (order.Details.Length > 10)
            {
                return ValidateResult.NotValid;
            }
            if (!phoneFormat.IsMatch(order.CustomerPhone))
            {
                return ValidateResult.NotValid;
            }
            if (!pointIdFormat.IsMatch(order.ParcelPointId))
            {
                return ValidateResult.NotValid;
            }

            return ValidateResult.Ok;
        }

    }
}