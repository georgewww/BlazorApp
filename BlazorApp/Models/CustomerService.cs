using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customer;
        public CustomerService(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _customer = database.GetCollection<Customer>(settings.CollectionName);
        }
        public async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                await _customer.InsertOneAsync(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomer(string id)
        {
            try
            {
                await _customer.DeleteOneAsync(book => book.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditCustomer(string id, Customer customer)
        {
            try
            {
                await _customer.ReplaceOneAsync(book => book.Id == id, customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                return await _customer.Find(customer => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Customer> SingleCustomer(string id)
        {
            try
            {
                return await _customer.Find<Customer>(customer => customer.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
