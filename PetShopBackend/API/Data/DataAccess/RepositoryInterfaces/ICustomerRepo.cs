using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Data.DataAccess.RepositoryInterfaces
{
    public interface ICustomerRepo : IRepository<Customer>
    {
        Task<ActionResult<Customer>> GetCustomerAsync(string username);
        Task<ActionResult<Customer>> GetCustomerForUpdates(string username);
        Task<IEnumerable<Order>> GetCustomerOrders(string customerName);

    }
}