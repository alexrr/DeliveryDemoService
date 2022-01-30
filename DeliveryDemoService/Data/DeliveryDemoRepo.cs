using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDemoService.Models;

namespace DeliveryDemoService.Data
{
    public class DeliveryDemoRepo : IDeliveryDemoRepo
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Order> _orderValidator;

        public DeliveryDemoRepo(AppDbContext context, IValidator<Order> validator)
        {
            _context = context;
            _orderValidator = validator;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public Order GetOrderId(int id)
        {
            var ord = _context.Orders.FirstOrDefault(order => order.Id == id);
            return ord;
        }

        public CreateOrderResult CreateOrder(Order order)
        {
            var validateResult = _orderValidator.Validate(order);
            switch (validateResult)
            {
                case ValidateResult.Ok:
                    _context.Orders.Add(order);
                    SaveChanges();
                    return CreateOrderResult.Success;
                case ValidateResult.Forbidden:
                    return CreateOrderResult.Forbidden;
                case ValidateResult.NotFound:
                    return CreateOrderResult.Error;
                case ValidateResult.NotValid:
                    return CreateOrderResult.Error;
                default:
                    return CreateOrderResult.Error;
            }
        }

        public UpdateOrderResult UpdateOrder(int id, ref Order order)
        {
            var savedOrder = _context.Orders.FirstOrDefault(order1 => order1.Id == id);
            if (savedOrder == null) return UpdateOrderResult.NotFound;
            savedOrder.Update(order);
            order = savedOrder;
            SaveChanges();
            return UpdateOrderResult.Success;
        }

        public void CancelOrder(int id)
        {
            var ord = _context.Orders.FirstOrDefault(order => order.Id == id);
            if (ord == null || ord.Status == OrderStatus.Cancelled) return;
            ord.Status = OrderStatus.Cancelled;
            SaveChanges();
        }

        public IEnumerable<ParcelPoint> GetAllParcelPoints(Func<ParcelPoint, string> orderPredicate)
        {
            var points = _context.ParcelPoints.OrderBy(orderPredicate);
            return points;
        }

        public ParcelPoint GetParcelPoint(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            var point = _context.ParcelPoints.FirstOrDefault(point => string.Equals(point.Id, id));
            return point;
        }
    }

    public enum UpdateOrderResult
    {
        Success,
        NotFound,
        Error
    }
}