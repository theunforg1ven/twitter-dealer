using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

		private readonly ApplicationSettings _applicationSettings;

		public ApplicationUserController(UserManager<ApplicationUser> userManager, 
			SignInManager<ApplicationUser> signInManager,
			IOptions<ApplicationSettings> options)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_applicationSettings = options.Value;
		}

		// POST: /api/applicationuser/register
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> RegisterUser([FromBody] ApplicationUserModel userModel)
		{
			var applicationUser = new ApplicationUser()
			{
				UserName = userModel.UserName,
				TwitterUsername = userModel.TwitterUsername,
				Email = userModel.Email
			};

			var result = await _userManager.CreateAsync(applicationUser, userModel.Password);

			if (result.Succeeded)
				return Ok(result);

			// TODO: log errors using result.Error

			return BadRequest();
		}

		// POST: /api/applicationuser/Login
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginModel login)
		{
			var user = await _userManager.FindByNameAsync(login.UserName);
			var key = Encoding.UTF8.GetBytes(_applicationSettings.JwtSecret);

			if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
			{
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
						new Claim("UserId", user.Id.ToString()),
					}),
					Expires = DateTime.UtcNow.AddDays(5),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};

				var tokenHandler = new JwtSecurityTokenHandler();
				var securityToken = tokenHandler.CreateToken(tokenDescriptor);
				var token = tokenHandler.WriteToken(securityToken);

				return Ok(new { token });
			}
			else
			{
				return BadRequest(new { message = "Username or password is incorrect" });
			}
		}
	}
}
