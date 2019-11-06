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

		private readonly ISaveThreadRepository _saveThreadRepository;

		public TwitterDataController(ITweetDataService tweetDataService,
									 ISaveThreadRepository saveThreadRepository)
		{
			_tweetDataService = tweetDataService;
			_saveThreadRepository = saveThreadRepository;
		}

		[HttpGet]
		[Route("TwitterData")]
		public async Task<StatusTweet> GetUserTweetsAsync(string tweetUrl)
		{
			var infoResult = await _tweetDataService.GetUserTweetsAsync(tweetUrl);

			//var isAdded = await _saveThreadRepository.AddThreadAsync(infoResult);

			return infoResult;
		}
	}
}
