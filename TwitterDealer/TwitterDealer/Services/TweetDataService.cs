using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Interfaces;
using TwitterDealer.Models.TwitterUserModels;
using TwitterDealer.TwitterApi;

namespace TwitterDealer.Services
{
	public class TweetDataService : ITweetDataService
	{
		private readonly TwitterService _twitterService;
		public TweetDataService()
		{
			_twitterService = AuthInit.TwitterService;
		}
		public async Task<TwitterStatus> GetUserTweetsAsync(string tweetUrl)
		{
			var tweetId = tweetUrl.Substring(tweetUrl.LastIndexOf('/') + 1);

			var tweet = await _twitterService.GetTweetAsync(new GetTweetOptions
			{
				Id = Convert.ToInt64(tweetId),
				IncludeEntities = true,
				IncludeMyRetweet = true,
			});

			var result = tweet.Value;


			return result;
		}
	}
}
