using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DTOs;
using API.Data.Model;
using API.helpers;

namespace API.Data.DataAccess.RepositoryInterfaces
{
    public interface IAnimalRepo : IRepository<Animal>
    {
        
        Task<Animal> GetAnimalEagerAsync(int id);
        Task<IEnumerable<string>> GetCategoriesAsync(); 

        Task<IEnumerable<Animal>> GetCategoryAnimalsAsync(string category);

        Task<IEnumerable<CartAnimalDto>> GetCartAnimals(string username);

        Task<IEnumerable<Animal>> GetAnimalsForCheckout(Customer customer);

        Task<PagedList<Animal>> GetPagedAnimalsAsync(AnimalQueryParams queryParams);

        Task RemoveByIdAsync(int id);

        Task ArchiveAnimalAsync(int id);
    }
}