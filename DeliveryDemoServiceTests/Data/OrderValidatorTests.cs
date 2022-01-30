using DeliveryDemoService.Data;
using DeliveryDemoService.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DeliveryDemoServiceTests.Data
{
    [TestFixture()]
    public class OrderValidatorTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DeliveryDemoServiceTest")
                .Options;
            dbContext = new AppDbContext(options);
            dbContext.Orders.Add(orderStored);
            dbContext.ParcelPoints.Add(parcelPointStored);
            dbContext.ParcelPoints.Add(parcelPointStoredBad);
            dbContext.SaveChanges();
            validator = new OrderValidator(dbContext);
        }

        private AppDbContext dbContext;
        private OrderValidator validator;

        private readonly ParcelPoint parcelPointStored = new ParcelPoint()
        {
            Id = "1920-001",
            Status = true,
            Address = "Address1"
        };

        private readonly ParcelPoint parcelPointStoredBad = new ParcelPoint()
        {
            Id = "1920-404",
            Status = false,
            Address = "Address2"
        };

        private readonly Order orderStored = new()
        {
            Details = new string[] { "pink socks", "red box", "silk pen" },
            Value = 1011,
            ParcelPointId = "1920-001",
            CustomerPhone = "+7910-001-02-03",
            CustomerFullName = "Alex"
        };

        private readonly Order order = new()
        {
            Details = new string[] { "blue socks", "black box", "red pen" },
            Value = 1011,
            ParcelPointId = "1920-001",
            CustomerPhone = "+7910-001-02-03",
            CustomerFullName = "Alex"
        };

        [Test()]
        public void ValidateOkTest()
        {
            Assert.AreEqual(ValidateResult.Ok, validator.Validate(order));
        }

        [Test()]
        public void ValidatePhoneTest()
        {
            order.CustomerPhone = "+8910-001-02-03";
            Assert.AreEqual(ValidateResult.NotValid, validator.Validate(order));
            order.CustomerPhone = "7910-001-02-03";
            Assert.AreEqual(ValidateResult.NotValid, validator.Validate(order));
            order.CustomerPhone = "+7910001-02-03";
            Assert.AreEqual(ValidateResult.NotValid, validator.Validate(order));
            order.CustomerPhone = "+7910-a01-02-03";
            Assert.AreEqual(ValidateResult.NotValid, validator.Validate(order));
            order.CustomerPhone = "+7910-101-02-03b";
            Assert.AreEqual(ValidateResult.NotValid, validator.Validate(order));
        }
        [Test()]
        public void ValidatePointTest()
        {
            order.ParcelPointId = "8910+001";
            Assert.AreEqual(ValidateResult.NotValid, validator.Validate(order));
            order.ParcelPointId = "8910-001";
            Assert.AreEqual(ValidateResult.NotValid, validator.Validate(order));
            order.ParcelPointId = "1920-404";
            Assert.AreEqual(ValidateResult.Forbidden, validator.Validate(order));
        }
    }
}