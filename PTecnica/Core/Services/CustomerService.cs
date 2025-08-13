

using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<Customer> CreateCustomer(Customer customer)
        {
            return _customerRepository.AddAsync(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            await (_customerRepository.DeleteAsync(id));
        }

        public Task<IReadOnlyList<Customer>> GetAllCustomers()
        {
            return _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task UpdateCustomer(Customer customer)
        {
             await _customerRepository.UpdateAsync(customer);
        }
    }
}
