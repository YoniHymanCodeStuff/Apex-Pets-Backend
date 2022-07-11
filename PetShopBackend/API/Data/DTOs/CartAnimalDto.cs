using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Model;

namespace API.Data.DTOs
{
    public class CartAnimalDto
    {
            
        public int Id { get; set; }
        public string Species { get; set; }

        public Photo MainPhoto {get;set; }

        public string Required_License { get; set; }

        public decimal price { get; set; }
                   
        
    
    }
}