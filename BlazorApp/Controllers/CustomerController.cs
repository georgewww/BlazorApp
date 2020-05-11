using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerservice;
        public CustomerController(ICustomerService customerservice)
        {
            _customerservice = customerservice;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<List<Customer>> GetAsync()
        {
            return await _customerservice.GetCustomers();
         
        }

        // GET: api/Customer/5
        [HttpGet("CustomersPreviousPage/{id}", Name = "CustomersPreviousPage")]
        public async Task<List<Customer>> GetCustomersPreviousPage(string id)
        {
            return await _customerservice.GetCustomersPreviousPage(id);
           
        }

        // GET: api/Customer/5
        [HttpGet("CustomersNextPage/{id}", Name = "CustomersNextPage")]
        public async Task<List<Customer>> GetCustomersNextPage(string id)
        {
            return await _customerservice.GetCustomersNextPage(id);
       
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            return await _customerservice.SingleCustomer(id);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task PostAsync([FromBody] Customer value)
        {
           await _customerservice.CreateCustomer(value);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] Customer value)
        {
            await _customerservice.EditCustomer(id, value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _customerservice.DeleteCustomer(id);
        }
    }
}
