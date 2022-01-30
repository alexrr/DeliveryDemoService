namespace DeliveryDemoService.Models
{
    public enum OrderStatus
    {
        /// <summary>
        /// Зарегистрирован 
        /// </summary>
        Registered = 1,
        /// <summary>
        /// Принят на складе 
        /// </summary>
        AcceptedInStock = 2,
        /// <summary>
        /// Выдан курьер
        /// </summary>
        IssuedToCourier = 3,
        /// <summary>
        /// Доставлен в постамат 
        /// </summary>
        DeliveredToParcelPoint = 4,
        /// <summary>
        /// Доставлен получателю
        /// </summary>
        DeliveredToCustomer = 5,
        /// <summary>
        /// Отменен
        /// </summary>
        Cancelled = 6
    }
}
