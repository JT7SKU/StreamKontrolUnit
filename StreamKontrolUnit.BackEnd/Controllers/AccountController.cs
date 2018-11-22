using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamKontrolUnit.BackEnd.Interface;
using StreamKontrolUnit.Shared;

namespace StreamKontrolUnit.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(IJwtTokenService jwtTokenService, UserManager<IdentityUser> userManager)
        {
           // this._tokenService = jwtTokenService;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] TokenViewModel vm)
        {
            if (ModelState.IsValid== false)
            {
                return BadRequest();
            }
            var result = await _userManager.CreateAsync(new IdentityUser()
            {
                UserName = vm.Email,
                Email = vm.Email
            }, vm.Password);
            if (result.Succeeded == false)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] TokenViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(vm.Email);
            var correctUser = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (correctUser==false)
            {
                return BadRequest("username or password is incorrect");
            }
            return Ok(new { token = GenerateToken(vm.Email) });
        }

        // Generates a token from the token service and returns it as a string
        public string GenerateToken(string email)
        {
            var token = _tokenService.BuildToken(email);

            return token;
        }
    }
}