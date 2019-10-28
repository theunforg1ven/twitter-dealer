﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Interfaces;
using TwitterDealer.TwitterApi;

namespace TwitterDealer.Services
{
	public class UserService : IUserService
	{
		private readonly TwitterService _twitterService;
		public UserService()
		{
			_twitterService = AuthInit.TwitterService;
		}

		public TwitterUser GetUserInfo()
		{
			var user = _twitterService.GetUserProfileFor(new GetUserProfileForOptions
			{ 
				ScreenName = "batyagaming"
			});

			return user;
		}

		public IEnumerable<TwitterStatus> GetUserTweets()
		{
			var currentTweets = _twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = "batyagaming",
				Count = 10,
				IncludeRts = false,
				ExcludeReplies = true
			});

			return currentTweets;
		}

		public IEnumerable<TwitterMedia> GetUserMedia()
		{
			var media = _twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = "batyagaming",
				Count = 100,
				IncludeRts = false,
				ExcludeReplies = true
			}).Where(tw => tw.Entities.Media != null)
			  .Select(tw => tw.Entities.Media)
			  .SelectMany(m => m);


			return media;
		}
	}
}
