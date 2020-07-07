using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TwitterDealer.Data.Entities;
using TwitterDealer.Dtos;
using TwitterDealer.Interfaces;

namespace TwitterDealer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthRepository _authRepo;

		private readonly IConfiguration _config;

		public AuthController(IAuthRepository repository, IConfiguration config)
		{
			_authRepo = repository;
			_config = config;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
		{
			userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

			if (await _authRepo.UserExists(userForRegisterDto.Username))
				return BadRequest("Username already exists");

			var userToCreate = new User
			{
				Username = userForRegisterDto.Username
			};

			var createdUser = await _authRepo.Register(userToCreate, userForRegisterDto.Password);

			//var userToReturn = _mapper.Map<UserForDetailedDto>(createdUser);

			//return CreatedAtRoute("GetUser", new { controller = "Users", id = createdUser.Id }, userToReturn);

			return StatusCode(201);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
		{
			var userFromRepo = await _authRepo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

			if (userFromRepo == null)
				return Unauthorized();

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
				new Claim(ClaimTypes.Name, userFromRepo.Username)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("ApplicationSettings:JwtSecret").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			//var user = _mapper.Map<UserForListDto>(userFromRepo);

			return Ok(new
			{
				token = tokenHandler.WriteToken(token)
			});
		}
	}
}
