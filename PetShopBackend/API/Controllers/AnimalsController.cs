using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.DataAccess;
using API.Data.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using API.utilities;
using API.Controllers;
using API.Data.DTOs;

namespace PetShop.PetShopBackend.API.Controllers
{

    public class AnimalsController : BaseApiController
    {
        
        private readonly IUoW _uow;
        
        public AnimalsController(IUoW uow)
        {
            _uow = uow;
                                    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            var animals = await _uow.animals.GetAllAsync();
            return Ok(animals);
            
        }



        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetAnimalCategories()
        {
            var cates = await _uow.animals.GetCategoriesAsync();
            
            // foreach (var i in cates)
            // {
            //     utils.DebugMsg(i);
            // }     

            return Ok(cates);
            
        }

        [HttpGet("Categories/{category}")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalsByCategory(string category)
        {
                        
            var CategoryAnimals = await _uow.animals.GetCategoryAnimalsAsync(category);
             
            return Ok(CategoryAnimals);

            
        }

        [HttpGet("CartAnimals/{customerName}")]
        public async Task<ActionResult<ICollection<CartAnimalDto>>> GetCartAnimalsAsync(string customerName)
        {
            //this is not secure currently... 
            
            var cartAnimals = await _uow.animals.GetCartAnimals(customerName);
            
            return Ok(cartAnimals);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
                        
            var animal = await _uow.animals.GetAnimalEagerAsync(id);

                         
            return animal;

            
        }

    }
}