using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Work3.Core.Entities;
using Work3.WebApi.Services;

namespace Work3.WebApi.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
	{
		private IUserService _userService;
	    public UserController(IUserService userService)
	    {
		    _userService = userService;
	    }

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]User userParam)
		{
			var user = _userService.Authenticate(userParam.Username, userParam.Password);

			if (user == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(user);
		}

		[Authorize(Roles = Role.Admin)]
		[HttpGet("getUsers")]
		public IActionResult GetAllUsers()
		{
			var users = _userService.GetAll();

			if (!users.Any())
			{
				return BadRequest(new {message = "No users in repository"});
			}

			return Ok(users);
		}
	}

}