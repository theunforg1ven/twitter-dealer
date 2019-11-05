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
		public async Task<IEnumerable<StatusTweet>> GetUserTweetsAsync(string tweetUrl)
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
				Q = $"@HoneyMadTV",
				Count = 100,
				Resulttype = TwitterSearchResultType.Mixed,
				SinceId = Convert.ToInt64(tweetId),
				IncludeEntities = true
			});

			var test = currentTweets.Value.Statuses.Count();

			var search = currentTweets.Value.Statuses
				.Where(tw => tw.InReplyToStatusId == Convert.ToInt64(tweetId))
				.Select(tw => new StatusTweet
				{
					IsFavourite = tw.IsFavorited,
					RetweetCount = tw.RetweetCount,
					TweetText = tw.Text,
					Url = $"https://twitter.com/{tw.User.ScreenName}/status/{tw.IdStr}",
					Language = tw.Language,
					IsPossiblySensitive = tw.IsPossiblySensitive,
					Created = tw.CreatedDate,
				});

			return search;

			//while (true)
			//{
			//	var currentReplies = await _twitterService.SearchAsync(new SearchOptions
			//	{
			//		Q = $"@HoneyMadTV",
			//		Count = 100,
			//		Resulttype = TwitterSearchResultType.Mixed,
			//		SinceId = Convert.ToInt64(tweetId),
			//		IncludeEntities = true,
			//		MaxId = maxId ?? null
			//	});

			//	foreach (var reply in currentReplies.Value.Statuses)
			//	{
			//		if (reply.InReplyToStatusId == Convert.ToInt64(tweetId))
			//		{
			//			list.Add(new StatusTweet
			//			{
			//				IsFavourite = reply.IsFavorited,
			//				RetweetCount = reply.RetweetCount,
			//				TweetText = reply.Text,
			//				Url = $"https://twitter.com/{reply.User.ScreenName}/status/{reply.IdStr}",
			//				Language = reply.Language,
			//				IsPossiblySensitive = reply.IsPossiblySensitive,
			//				Created = reply.CreatedDate,
			//			});

			//			foreach (var replyToReply in await GetUserTweetsAsync(tweetUrl))
			//			{
			//				list.Add(replyToReply);
			//			}

			//			maxId = reply.Id;
			//		}
			//	}

			//	if (currentReplies.Value.Statuses.Count() != 100)
			//		break;
			//}

			//return list;
		}

	}
}
