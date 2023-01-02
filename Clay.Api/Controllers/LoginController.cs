﻿using Clay.Api.Models;
using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest model)
        {
            try
            {
                var login = await _loginService.GetByEmailAndPassword(model.Email, model.Password);

                var token = _loginService.GenerateToken(login);

                return new LoginResponse
                {
                    Id = login.Id,
                    Email = login.Email,
                    Token = token
                };
            }
            catch (EntityNotFoundException)
            {
                var errorDetails = new ErrorDetails
                {
                    StatusCode = 400,
                    Message = "Email/Password incorrect or invalid."
                };

                return BadRequest(errorDetails);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] LoginCreationRequest request)
        {
            var login = request.Adapt<LoginDTO>();

            await _loginService.CreateLogin(login);

            return NoContent();
        }

        [HttpPatch]
        [Authorize]
        [Route("{id:int}/ChangePassword")]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest request)
        {
            var loginToChangePassword = request.Adapt<ChangePasswordDTO>();
            loginToChangePassword.Id = id;

            await _loginService.ChangePassword(loginToChangePassword);

            return NoContent();
        }
    }
}
