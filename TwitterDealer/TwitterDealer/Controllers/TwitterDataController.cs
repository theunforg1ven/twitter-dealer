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
	public class TwitterDataController : ControllerBase
	{
		private readonly ITweetDataService _tweetDataService;

		public TwitterDataController(ITweetDataService tweetDataService)
		{
			_tweetDataService = tweetDataService;
		}

		[HttpGet]
		[Route("TwitterData")]
		public async Task<StatusTweet> GetUserTweetsAsync(string tweetUrl)
		{
			var infoResult = await _tweetDataService.GetUserTweetsAsync(tweetUrl);

			return infoResult;
		}
	}
}
