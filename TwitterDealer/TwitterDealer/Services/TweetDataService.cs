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

			var list = new List<TwitterStatus>();
		
			var currentTweets = await _twitterService.SearchAsync(new SearchOptions
			{
				Q = $"@evrlstng_winter",
				Count = 100,
				Resulttype = TwitterSearchResultType.Mixed,
				SinceId = Convert.ToInt64(tweetId),
				IncludeEntities = true
			});

			var test = currentTweets.Value.Statuses.Count();

			var search = currentTweets.Value.Statuses
				.Where(tw => tw.InReplyToStatusId == Convert.ToInt64(tweetId)).ToList();

			list.AddRange(search);

			foreach (var searchItem in search)
				list.AddRange(GetReplyToReplies(searchItem));

			var returnSearch = list
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

			return returnSearch;
		}

		private IEnumerable<TwitterStatus> GetReplyToReplies(TwitterStatus searchResult)
		{
			var tempStatuses = new List<TwitterStatus>();
   
			var currentTweets =  _twitterService.Search(new SearchOptions
			{
				Q = $"@{searchResult.User.ScreenName}",
				Count = 100,
				Resulttype = TwitterSearchResultType.Mixed,
				SinceId = searchResult.Id,
				IncludeEntities = true
			});

			var search = currentTweets.Statuses
				.Where(tw => tw.InReplyToStatusId == Convert.ToInt64(searchResult.Id)).ToList();

			if (search.Count > 0)
			{
				foreach (var searchItem in search)
					tempStatuses.Add(searchItem);
			}

			return tempStatuses ?? Enumerable.Empty<TwitterStatus>();
		}

	}
}
