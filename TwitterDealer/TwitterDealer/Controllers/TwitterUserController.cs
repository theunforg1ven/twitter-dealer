using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Interfaces;

namespace TwitterDealer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TwitterUserController : ControllerBase
	{
		private readonly IUserService _userService;
		public TwitterUserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Route("TwitterUserProfile")]
		public TwitterUser GetUserInfoAsync()
		{
			var infoResult =  _userService.GetUserInfo();

			return infoResult;
		}

		[HttpGet]
		[Route("TwitterUserTweets")]
		public IEnumerable<TwitterStatus> GetUserTweetsAsync()
		{
			var infoResult = _userService.GetUserTweets();

			return infoResult;
		}

		[HttpGet]
		[Route("TwitterUserMedia")]
		public IEnumerable<TwitterMedia> GetUserMedia()
		{
			var media = _userService.GetUserMedia();

			return media;
		}
	}
}
