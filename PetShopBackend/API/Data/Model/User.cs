using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public abstract class User
    {
        public int Id {get;set;}

        public string FirstName {get;set;} = "";
        public string LastName {get;set;} = "";

        public string UserName { get; set; }

        public byte[] hash { get; set; }

        public byte[] salt { get; set; }

        public string Email {get;set;} = "";

        public Photo Avatar { get; set;} 
        public string UserType { get; internal set; } 
    }
}