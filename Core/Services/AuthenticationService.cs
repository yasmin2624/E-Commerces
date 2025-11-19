using Domain.Entities.Identity_Modules;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Abstractions;
using Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration Configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //Check if user exists
            var User = await userManager.FindByEmailAsync(loginDto.Email);
            if (User == null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }

            //Check password
            var isPasswordValid = await userManager.CheckPasswordAsync(User, loginDto.Password);
            if (isPasswordValid)
            {
                return new UserDto
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }


        }
        

        async Task<UserDto> IAuthenticationService.RegisterAsync(RegisterDto registerDto)
        {
            //Mapping RegisterDto to ApplicationUser
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };

            //Create user
            var Result =await userManager.CreateAsync(user, registerDto.Password);
            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token =await CreateTokenAsync(user)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestExeption(Errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name , user.UserName!),
                new(ClaimTypes.NameIdentifier, user.Id)
            };
            var Roles = await userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecretKey = Configuration.GetSection("JWTOptions")["SecretKey"];
            var Key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: Configuration["JWTOptions.Issuer"],
                audience: Configuration["JWTOptions.Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }


    }

}
