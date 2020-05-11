using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers();
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> EditCustomer(string id, Customer customer);
        Task<Customer> SingleCustomer(string id);
        Task<bool> DeleteCustomer(string id);
        Task<List<Customer>> GetCustomersNextPage(string id);
        Task<List<Customer>> GetCustomersPreviousPage(string id);
        
    }
}
