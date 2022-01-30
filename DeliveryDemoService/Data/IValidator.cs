namespace DeliveryDemoService.Data
{
    public interface IValidator<in T>
    {
        ValidateResult Validate(T obj);
    }
}
