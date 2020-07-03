using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Data.Entities;
using TwitterDealer.Interfaces;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TwitterDataController : ControllerBase
	{
		private readonly ITweetDataService _tweetDataService;

		private readonly ISaveThreadRepository _saveThreadRepository;
		
		private readonly UserManager<ApplicationUser> _userManager;

		public TwitterDataController(ITweetDataService tweetDataService,
									 ISaveThreadRepository saveThreadRepository,
									 UserManager<ApplicationUser> userManager)
		{
			_tweetDataService = tweetDataService;
			_saveThreadRepository = saveThreadRepository;
			_userManager = userManager;
		}

		[HttpGet]
		//[Authorize] // send access token to authorize
		[Route("TwitterData")]
		public async Task<StatusTweet> GetUserTweetsAsync(string tweetUrl)
		{
			var infoResult = await _tweetDataService.GetUserTweetsAsync(tweetUrl);

			var userId = User.Claims.First(c => c.Type == "UserId")?.Value;

			var isAdded = await _saveThreadRepository.AddThreadAsync(infoResult, userId);

			return infoResult;
		}
	}
}
