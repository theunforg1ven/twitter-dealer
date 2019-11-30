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

		private readonly UserManager<ApplicationUser> _userManager;

		public TwitterThreadController(ITweetThreadService tweetThreadService,
									 ISaveThreadRepository saveThreadRepository,
									 UserManager<ApplicationUser> userManager)
		{
			_tweetThreadService = tweetThreadService;
			_saveThreadRepository = saveThreadRepository;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("GetTwitterThread")]
		public async Task<StatusTweet> GetUserTweetsAsync(string tweetUrl)
		{
			var infoResult = await _tweetThreadService.GetUserThreadAsync(tweetUrl);

			return infoResult;
		}
	}
}
