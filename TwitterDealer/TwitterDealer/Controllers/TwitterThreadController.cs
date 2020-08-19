using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Data.Entities;
using TwitterDealer.Interfaces;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TwitterThreadController : ControllerBase
	{
		private readonly ITweetThreadService _tweetThreadService;

		private readonly ISaveThreadRepository _saveThreadRepository;

		public TwitterThreadController(ITweetThreadService tweetThreadService,
									 ISaveThreadRepository saveThreadRepository
									 )
		{
			_tweetThreadService = tweetThreadService;
			_saveThreadRepository = saveThreadRepository;
			
		}

		[HttpGet]
		[Route("gettwitterthread")]
		public async Task<StatusTweet> GetUserTweetsAsync(string tweetUrl)
		{
			var infoResult = await _tweetThreadService.GetUserThreadAsync(tweetUrl);

			return infoResult;
		}
	}
}
