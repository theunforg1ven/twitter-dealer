using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Dtos;
using TwitterDealer.Interfaces;

namespace TwitterDealer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IDealerRepository _dealerRepo;

		private readonly IMapper _mapper;

		public UsersController(IDealerRepository repo, IMapper mapper)
		{
			_dealerRepo = repo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _dealerRepo.GetUsers();
			var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

			return Ok(usersToReturn);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _dealerRepo.GetUser(id);
			var userToReturn = _mapper.Map<UserForDetailedDto>(user);

			return Ok(userToReturn);
		}
	}
}
