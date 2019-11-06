using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Data;
using TwitterDealer.Data.Entities;
using TwitterDealer.Interfaces;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Repositories
{
	public class SaveThreadRepository : ISaveThreadRepository
	{
		private readonly AppDbContext _appDbContext;

		private readonly UserManager<ApplicationUser> _userManager;

		private readonly IHttpContextAccessor _contextAccessor;

		public SaveThreadRepository(AppDbContext appDbContext,
									IHttpContextAccessor contextAccessor,
									UserManager<ApplicationUser> userManager)
		{
			_appDbContext = appDbContext;
			_contextAccessor = contextAccessor;
			_userManager = userManager;
		}

		public async Task<bool> AddThreadAsync(StatusTweet stTweet)
		{
			var thread = new SavedThread()
			{
				IsFavourite = stTweet.IsFavourite,
				FavoriteCount = stTweet.FavoriteCount,
				RetweetCount = stTweet.RetweetCount,
				TweetText = stTweet.TweetText,
				Url = stTweet.Url,
				Language = stTweet.Language,
				IsPossiblySensitive = stTweet.IsPossiblySensitive,
				Created = stTweet.Created,
				ApplicationUser = await GetCurrentUserAsync()
			};

			await _appDbContext.SavedThreads.AddAsync(thread);

			if (_appDbContext.SaveChanges() > 0)
				return true;

			return false;
		}

		private async Task<ApplicationUser> GetCurrentUserAsync()
		{
			var contextUser = _contextAccessor.HttpContext.User.Identity.Name;

			var user = await _userManager.FindByNameAsync(contextUser);

			return user;
		}
	}
}
