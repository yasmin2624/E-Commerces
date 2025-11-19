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




    }
}
