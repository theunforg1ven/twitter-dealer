using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Data.Entities;
using TwitterDealer.Models;

namespace TwitterDealer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ApplicationUserController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;

		private readonly SignInManager<ApplicationUser> _signInManager;

		public ApplicationUserController(UserManager<ApplicationUser> userManager, 
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// POST: /api/applicationUser/register
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> RegisterUser([FromBody] ApplicationUserModel userModel)
		{
			var applicationUser = new ApplicationUser()
			{
				UserName = userModel.UserName,
				Email = userModel.Email,
				TwitterUsername = userModel.TwitterUsername
			};

			var result = await _userManager.CreateAsync(applicationUser, userModel.Password);

			if (result.Succeeded)
				return Ok(result);

			// TODO: log errors using result.Error

			return BadRequest();
		}
	}
}
