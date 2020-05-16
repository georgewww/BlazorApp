using BlazorApp.Models;
using IdentityModel.Client;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace XUnitTestBlazorApp.CustomerControllerTests
{
    public class CustomerControllerIntegrationTests : IClassFixture<AppTestFixture>
    {
        readonly AppTestFixture _fixture;
        readonly HttpClient _client;
        public CustomerControllerIntegrationTests(AppTestFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.CreateClient();
        }
        [Theory]
        [InlineData("http://localhost:59918/api/customer/5eb941f4ce6a823450c1a667")]
        public async void Get_SingleCustomer_Valid_Success(string url)
        {
            // Arrange
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://demo.identityserver.io/");
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "m2m",
                ClientSecret = "secret",
                Scope = "api"
            });
       
            _client.SetBearerToken(tokenResponse.AccessToken);
            // Act
            var response = await _client.GetAsync(url);
            // Assert1
            Assert.NotNull(tokenResponse.AccessToken);
            // Assert2
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Assert3
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            // Assert4
            Assert.NotNull(response.Content);
            // Assert5
            var respmessage = await response.Content.ReadAsStringAsync();
            var bsonDocument = BsonDocument.Parse(respmessage);
            var actualcustomer = BsonSerializer.Deserialize<object>(bsonDocument); //BUG: Deserialize<Customer> doesnt work
         
            Assert.Equal("demoCompanyName", ((dynamic)actualcustomer).companyName);
            Assert.Equal("demoAddress", ((dynamic)actualcustomer).address);

        }
    }
}
