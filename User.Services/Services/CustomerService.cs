using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Repositories;
using User.Models.Models;

namespace User.Services.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<IEnumerable<Customer>> GetAll(string keyword);
        Task<Customer> Add(Customer entity);
        Task<Customer> GetById(string id);
        Task<Customer> Delete(string id);
        Task<Customer> Update(Customer entity);
    }
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerService(ICustomerRepository CustomerRepository)
        {
            this._CustomerRepository = CustomerRepository;
        }

        public async Task<Customer> Add(Customer entity)
        {
            return await _CustomerRepository.Add(entity);
        }

        public async Task<Customer> Delete(string id)
        {
            return await _CustomerRepository.Delete(id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _CustomerRepository.GetAll();
        }

        public async Task<IEnumerable<Customer>> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return await _CustomerRepository.GetAllByCondition(x => x.FirstName.Contains( keyword) || x.LastName.Contains(keyword));
            return await _CustomerRepository.GetAll();
        }

        public async Task<Customer> GetById(string id)
        {
            return await _CustomerRepository.GetById(id);
        }


        public async Task<Customer> Update(Customer entity)
        {
            return await _CustomerRepository.Update(entity);
        }
    }
}
