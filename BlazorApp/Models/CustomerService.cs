using MongoDB.Bson;
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
                await _customer.DeleteOneAsync(book => book.Id == ObjectId.Parse(id));
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
                await _customer.ReplaceOneAsync(book => book.Id == ObjectId.Parse(id), customer);
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
               return await _customer.Find(customer => true).SortByDescending(p => p.Id).Limit(5).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Customer>> GetCustomersNextPage(string id)
        {
            try
            {
                var filterBuilder = Builders<Customer>.Filter;
                var filter = filterBuilder.Lt(customer => customer.Id, ObjectId.Parse(id));
             
                return await _customer.Find(filter)
                                            .SortByDescending(p => p.Id)
                                            .Limit(5)
                                            .ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Customer>> GetCustomersPreviousPage(string id)
        {
            try
            {
                var filterBuilder = Builders<Customer>.Filter;
                var filter = filterBuilder.Gt(customer => customer.Id, ObjectId.Parse(id));

                var temp  =  await _customer.Find(filter)
                                            .SortBy(p => p.Id)
                                            .Limit(5)
                                            .ToListAsync();
                return   temp.OrderByDescending(o => o.Id).ToList();
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
                return await _customer.Find<Customer>(customer => customer.Id.Equals(ObjectId.Parse(id))).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
