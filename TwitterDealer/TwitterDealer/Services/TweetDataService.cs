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
		public async Task<IEnumerable<TwitterStatus>> GetUserTweetsAsync(string tweetUrl)
		{
			var tweetId = tweetUrl.Substring(tweetUrl.LastIndexOf('/') + 1);

			var tweet = await _twitterService.GetTweetAsync(new GetTweetOptions
			{
				Id = Convert.ToInt64(tweetId),
				IncludeEntities = true,
				IncludeMyRetweet = false,
			});

			var result = tweet.Value;

			var list = new List<StatusTweet>();
		
			var currentTweets = await _twitterService.SearchAsync(new SearchOptions
			{
				Q = $"@elen_de_len",
				Count = 100,
				Resulttype = TwitterSearchResultType.Mixed,
				SinceId = Convert.ToInt64(tweetId),
				IncludeEntities = true
			});

			var test = currentTweets.Value.Statuses.Count();

			var search = currentTweets.Value.Statuses
				.Where(tw => tw.InReplyToStatusId == Convert.ToInt64(tweetId));
				//.Select(tw => new StatusTweet
				//{
				//	IsFavourite = tw.IsFavorited,
				//	RetweetCount = tw.RetweetCount,
				//	TweetText = tw.Text,
				//	Url = $"https://twitter.com/{tw.User.ScreenName}/status/{tw.IdStr}",
				//	Language = tw.Language,
				//	IsPossiblySensitive = tw.IsPossiblySensitive,
				//	Created = tw.CreatedDate,
				//});

			return search;
		}

		//private IEnumerable<TwitterStatus> GetReplyToReplies(IEnumerable<TwitterStatus> searchResult)
		//{

		//}

	}
}
