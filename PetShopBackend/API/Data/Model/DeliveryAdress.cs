using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public class DeliveryAdress
    {
        public int Id {get;set;}
        public string Country {get;set;} = "";
        public string City { get; set; } = "";
        public string Street { get; set; }= "";

        public int houseNumber {get;set;}

        public string Zip {get;set;}
    }
}