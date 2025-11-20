using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.controllers
{
    public  class AuthenticationController(IServiceManager serviceManager) :APIBaseController
    {
        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }

        #endregion

        #region CheckEmail
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }
        #endregion

        #region GetCurrentUser
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await serviceManager.AuthenticationService.GetCurrentUser(Email);
            return Ok(AppUser);

        }
        #endregion

        #region GetCurrentUserAddress
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await serviceManager.AuthenticationService.GetCurrentUserAddress(Email);
            return Ok(Address);
        }

        #endregion

        #region  UpdateCurrentUserAddress
        [Authorize]
        [HttpPut("Address")]

        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await serviceManager.AuthenticationService.UpdateCurrentUserAddress(addressDto, Email);
            return Ok(UpdatedAddress);
        }
        #endregion


    }
}
