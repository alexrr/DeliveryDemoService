using System;
using System.Collections.Generic;
using DeliveryDemoService.Models;

namespace DeliveryDemoService.Data
{
    public interface IDeliveryDemoRepo
    {
        bool SaveChanges();

        IEnumerable<Order> GetAllOrders();
        Order GetOrderId(int id);
        CreateOrderResult CreateOrder(Order order);
        UpdateOrderResult UpdateOrder(int id, ref Order order);
        void CancelOrder(int id);

        IEnumerable<ParcelPoint> GetAllParcelPoints(Func<ParcelPoint, string> orderPredicate);
        ParcelPoint GetParcelPoint(string id);
    }
}