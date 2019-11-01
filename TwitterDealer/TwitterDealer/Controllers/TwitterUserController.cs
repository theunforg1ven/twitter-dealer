using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Interfaces;
using TwitterDealer.Models.TwitterUserModels;

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
		public MainUserModel GetUserInfoAsync(string screenName)
		{
			var infoResult =  _userService.GetUserInfo(screenName);

			return infoResult;
		}

		[HttpGet]
		[Route("TwitterUserTweets")]
		public IEnumerable<StatusTweet> GetUserTweetsAsync(string screenName)
		{
			var infoResult = _userService.GetUserTweets(screenName);

			return infoResult;
		}

		[HttpGet]
		[Route("TwitterUserMedia")]
		public async Task<IEnumerable<UserMedia>> GetUserMediaAsync(string screenName, int mediaCount)
		{
			var media = await _userService.GetUserMediaAsync(screenName, mediaCount);

			return media;
		}
	}
}
