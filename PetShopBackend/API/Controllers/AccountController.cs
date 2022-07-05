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
using API.Data.Model;
using System.Security.Cryptography;
using API.Services.authentication;
using API.Data.DTOs;
using API.helpers;
using AutoMapper;

namespace PetShop.PetShopBackend.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUoW _uow;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IUoW uow, ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            this._mapper = mapper;
            _uow = uow;

        }

        [HttpPost("Registration")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {


            if (await UserExists(registerDto.Username)) return BadRequest("username not available");

            using var hmac = new HMACSHA512();


            var customer = new Customer
            {
                UserName = registerDto.Username,
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                salt = hmac.Key
            };
 
            _uow.customers.Add(customer);

            await _uow.Complete();

            return new UserDto { UserName = registerDto.Username, Token = _tokenService.CreateToken(customer) };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var user = await this._uow.users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            //checking for single user with this name in system: 
            if (user == null) return Unauthorized("Account does not exist");

            //validating pwd with hmac:
            using var hmac = new HMACSHA512(user.salt); //creating a hmac to use with the user input that already includes the salt 

            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.Password));//encrypting the user input with the salt from the username data. 

            //comparing the 2 hashcodes char by char (why can't we compare them as wholes?)
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.hash[i]) return Unauthorized("invalid password");
            }

            bool isAdmin = await _uow.users.CheckIfIsAdminAsync(loginDto.Username);


            return new UserDto { UserName = loginDto.Username,
             Token = _tokenService.CreateToken(user),
             IsAdmin = isAdmin };

        }

        // [HttpGet("UserData/{username}")]
        // public async Task<ActionResult<UserData_IsAdminDto>> GetUserData(string username){
           
        //     bool isAdmin = await _uow.users.CheckIfIsAdminAsync(username);
            
        //     UserData_IsAdminDto retval = new UserData_IsAdminDto{isAdmin = isAdmin};

        //     if(isAdmin){
        //         retval.admin =  await _uow.admins.SingleOrDefaultAsync(x=>x.UserName == username);
        //     }
        //     else
        //     {
        //         retval.customer =  await _uow.customers.GetCustomerAsync(username);
        //     }

        //     return retval;
        // }

        [HttpGet("Customer/{username}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(string username)
        {
            var cust =  (await _uow.customers.GetCustomerAsync(username)).Value;

            var custDto = _mapper.Map<CustomerDto>(cust);
            //cant get automapper to work.not sure why. 

            // var custDto = new CustomerDto (){
                
            // };

            return custDto;
        }  

        
        public async Task<bool> GetIsUserAdmin(string username){
           
            return await _uow.users.CheckIfIsAdminAsync(username);
        }

        private async Task<bool> UserExists(string username)
        {
            //I should probably create a repo function for this. 
            var user = await _uow.users.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());

            if (user == null) { return false; }
            return true;
        }

    }
}