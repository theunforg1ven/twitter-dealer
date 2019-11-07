using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Data.Entities;

namespace TwitterDealer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserProfileController : ControllerBase
	{
		private UserManager<ApplicationUser> _userManager;

		public UserProfileController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		// GET : /api/UserProfile
		[HttpGet]
		[Authorize] // send access token to authorize
		public async Task<object> GetUserProfile()
		{
			string userId = User.Claims.First(c => c.Type == "UserId").Value;

			var user = await _userManager.FindByIdAsync(userId);

			return new
			{
				user.TwitterUsername,
				user.Email,
				user.UserName
			};
		}
	}
}
