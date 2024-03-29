using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.DTOs;
using API.Data.Model;
using API.helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace API.Data.DataAccess.RepositoryClasses
{
    public class AnimalRepo : Repository<Animal>, IAnimalRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AnimalRepo(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Animal> GetAnimalEagerAsync(int id)
        {
            return await _context.Animals
            .Include(x => x.images)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
                       
            return await _context.Animals.Where(x=>x.IsArchived==false).Select(x => x.Category).Distinct().ToListAsync();
        }


        public async Task<IEnumerable<Animal>> GetAnimalsForCheckout(Customer customer)
        {
            var IDList = new List<int>();

            foreach (var cartItem in customer.ShoppingCart)
            {
                IDList.Add(cartItem.OrderedAnimalId);
            }

            return await _context.Animals.Where(x => IDList.Contains(x.Id)).ToListAsync();
        }

        public async Task<IEnumerable<CartAnimalDto>> GetCartAnimals(string username)
        {

            var cart = await _context.Customers.Where(x => x.UserName == username)
            .Select(x => x.ShoppingCart).SingleOrDefaultAsync();

            var relevantIds = cart.Select(x => x.OrderedAnimalId);

            var animalBank = await _context.Animals
            .Where(x => relevantIds.Contains(x.Id)).ToListAsync();

            var cartAnimals = new List<CartAnimalDto>();

            foreach (var cartItem in cart)
            {
                var animal = animalBank.SingleOrDefault(x => x.Id == cartItem.OrderedAnimalId);

                var cartAnimal = _mapper.Map<CartAnimalDto>(animal);

                cartAnimal.animalId = animal.Id;
                cartAnimal.cartItemId = cartItem.Id;

                cartAnimals.Add(cartAnimal);
            }

            return cartAnimals;
        }

        public async Task<PagedList<Animal>> GetPagedAnimalsAsync(AnimalQueryParams queryParams)
        {
            var query = _context.Animals.AsQueryable();
            query = query.Where(x => !x.IsArchived);

            if (!string.IsNullOrEmpty(queryParams.Category))
            {
                query = query.Where(x => x.Category == queryParams.Category);
            }

            if(!string.IsNullOrEmpty(queryParams.SearchString))
            {
                query = query.Where(x=>x.Name.ToLower().Contains(queryParams.SearchString.ToLower()));
            }

            if(queryParams.MinPrice != null)
            {
                query = query.Where(x=>x.price>=queryParams.MinPrice);
            }

            if(queryParams.MaxPrice != null)
            {
                query = query.Where(x=>x.price<=queryParams.MaxPrice);
            }

                        
            if(queryParams.OrderBy != null)
            {
                
                switch (queryParams.OrderBy)
                {
                    case "id":
                    query = query.OrderBy(x=>x.Id);
                    break;
           
                    case "name":
                    query =query.OrderBy(x=>x.Name);
                    break;
       
                    case "category":
                    query =query.OrderBy(x=>x.Category);
                    break;

                    case "price":
                    query =query.OrderBy(x=>(float)x.price);
                    break;

                    case "required_Habitat":
                    query =query.OrderBy(x=>x.Required_Habitat);
                    break;

                    case "species":
                    query =query.OrderBy(x=>x.Species);
                    break;

                    case "description":
                    query =query.OrderBy(x=>x.Description);
                    break;
            
                    default:
                    query =query.OrderBy(x=>x.Id);
                    break;
                }
            }

            if(queryParams.IsDescending)
            {query =query.Reverse();}

            return await PagedList<Animal>.CreateAsync
            (
                query.AsNoTracking().Include(x => x.images)
                .Include(x=>x.MainPhoto),
                queryParams.Pagenumber,
                queryParams.PageSize
            );
        }

        public async Task RemoveByIdAsync(int id)
        {
            var animal = await _context.Animals.Include(x => x.images).FirstOrDefaultAsync(x => x.Id == id);

            _context.Animals.Remove(animal);
        }

        public async Task ArchiveAnimalAsync(int id)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == id);

            animal.IsArchived = true;

        }

        public async Task<CartAnimalDto> GetCartAnimal(ShoppingCartItem item)
        {
            var BaseAnimal = await _context.Animals.FirstOrDefaultAsync(x=>x.Id == item.OrderedAnimalId);
            
            var cartAnimal = _mapper.Map<CartAnimalDto>(BaseAnimal);

            cartAnimal.animalId = item.OrderedAnimalId;
            cartAnimal.cartItemId = item.Id;

            return cartAnimal;
        }
    }
}