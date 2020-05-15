using BlazorApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestBlazorApp
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerService> _customerMock = new Mock<ICustomerService>();
       // private readonly Mock<IMongoDbSettings> _mongoSettingsMock = new Mock<IMongoDbSettings>();
        private readonly MongoDbSettings _mongoSettings = new MongoDbSettings();
        private readonly CustomerService _customerservice;

        public CustomerServiceTest()
        {
            //TODO: cant mock dbsettings, the _mongoSettingsMock.Object properties are null
            _mongoSettings.ConnectionString = "mongodb://localhost:27017";
            _mongoSettings.DatabaseName = "CustomerDB";
            _mongoSettings.CollectionName = "CustomerRecord";
             _customerservice = new CustomerService(_mongoSettings);
        }
    

        [Fact]
        public async Task SingleCustomer_ShouldReturnCustomer_CustomerExists()
        {
            // Arrange
            var customerMock = new Customer
            {
                Id =ObjectId.Parse("5eb941f4ce6a823450c1a667"),
                CompanyName = "demoCompanyName",
                ContactName = "demoContactName",
                Address = "demoAddress",
                City = "demoCity",
                Region = "demoRegion",
                PostalCode = "demoPostalCode"

            };
              _customerMock.Setup(x => x.SingleCustomer("5eb941f4ce6a823450c1a667")).ReturnsAsync(customerMock);

            // Act
              var customer = await _customerservice.SingleCustomer("5eb941f4ce6a823450c1a667");
            _customerMock.VerifyAll();
            // Assert

            Assert.Equal(customer.CompanyName, customerMock.CompanyName);
        }
    }
}
