using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

		public SaveThreadRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
		{
			_appDbContext = appDbContext;
			_userManager = userManager;
		}

		public async Task<bool> AddThreadAsync(StatusTweet stTweet, string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

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
				ApplicationUser = user
			};

			var isThreadExist = await _appDbContext.SavedThreads.FirstOrDefaultAsync(th => th.Url == thread.Url);

			if (isThreadExist == null)
			{
				await _appDbContext.SavedThreads.AddAsync(thread);
			}

			if (_appDbContext.SaveChanges() > 0)
				return true;

			return false;
		}
	}
}
