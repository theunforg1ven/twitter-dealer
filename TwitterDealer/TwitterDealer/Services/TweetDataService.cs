using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Helpers;
using TwitterDealer.Interfaces;
using TwitterDealer.Models.TwitterUserModels;
using TwitterDealer.TwitterApi;

namespace TwitterDealer.Services
{
	public class TweetDataService : ITweetDataService
	{
		private readonly TwitterService _twitterService;

		private readonly SelectMediaHelper _sMediaHelper;
		public TweetDataService()
		{
			_twitterService = AuthInit.TwitterService;
			_sMediaHelper = new SelectMediaHelper();
		}

		public async Task<StatusTweet> GetUserTweetsAsync(string tweetUrl)
		{
			var tweetId = tweetUrl.Substring(tweetUrl.LastIndexOf('/') + 1);

			var tweet = await _twitterService.GetTweetAsync(new GetTweetOptions
			{
				Id = Convert.ToInt64(tweetId),
				IncludeEntities = true,
				IncludeMyRetweet = false,
			});

			var resultTweetList = new List<StatusTweet>();

			var search = await GetCurrentRepliesAsync(tweet.Value);

			var querySearch = await GetTwitterStatusesAsync(tweet.Value);

			search.AddRange(querySearch);

			var statusTweets = search
				.Select(tw => new StatusTweet
				{
					IsFavourite = tw.IsFavorited,
					FavoriteCount = tw.FavoriteCount,
					RetweetCount = tw.RetweetCount,
					TweetText = tw.Text,
					Url = $"https://twitter.com/{tw.User.ScreenName}/status/{tw.IdStr}",
					Language = tw.Language,
					IsPossiblySensitive = tw.IsPossiblySensitive,
					Created = tw.CreatedDate,
					UserName = tw.User.Name,
					UserScreenName = tw.User.ScreenName,
					MediaUrl = _sMediaHelper.SelectMediaBase(tw)
				})
				.ToList();

			for (int i = 0; i < search.Count; i++)
			{
				statusTweets[i].Replies = (await GetReplyToRepliesAsync(search[i]));
			}

			resultTweetList.AddRange(statusTweets);

			var result = new StatusTweet
			{
				IsFavourite = tweet.Value.IsFavorited,
				RetweetCount = tweet.Value.RetweetCount,
				FavoriteCount = tweet.Value.FavoriteCount,
				TweetText = tweet.Value.Text,
				Url = $"https://twitter.com/{tweet.Value.User.ScreenName}/status/{tweet.Value.IdStr}",
				Language = tweet.Value.Language,
				IsPossiblySensitive = tweet.Value.IsPossiblySensitive,
				Created = tweet.Value.CreatedDate,
				UserName = tweet.Value.User.Name,
				UserScreenName = tweet.Value.User.ScreenName,
				Replies = resultTweetList,
				MediaUrl = _sMediaHelper.SelectMediaBase(tweet.Value)
			};

			return result;
		}

		private async Task<IEnumerable<StatusTweet>> GetReplyToRepliesAsync(TwitterStatus searchResult)
		{	
			var search = await GetCurrentRepliesAsync(searchResult);

			var querySearch = await GetTwitterStatusesAsync(searchResult);

			search.AddRange(querySearch);

			var statusTweets = search
				.Select(tw => new StatusTweet
				{
					IsFavourite = tw.IsFavorited,
					FavoriteCount = tw.FavoriteCount,
					RetweetCount = tw.RetweetCount,
					TweetText = tw.Text,
					Url = $"https://twitter.com/{tw.User.ScreenName}/status/{tw.IdStr}",
					Language = tw.Language,
					IsPossiblySensitive = tw.IsPossiblySensitive,
					Created = tw.CreatedDate,
					UserName = tw.User.Name,
					UserScreenName = tw.User.ScreenName,
					MediaUrl = _sMediaHelper.SelectMediaBase(tw)
				}).ToList();

			if (search.Count > 0)
			{
				for (int i = 0; i < search.Count; i++)
				{
					statusTweets[i].Replies = (await GetReplyToRepliesAsync(search[i]));
				}
			}

			return statusTweets ?? Enumerable.Empty<StatusTweet>();
		}

		private async Task<List<TwitterStatus>> GetCurrentRepliesAsync(TwitterStatus searchResult)
		{
			var currentTweets = await _twitterService.SearchAsync(new SearchOptions
			{
				Q = $"@{searchResult.User.ScreenName}",
				Count = 100,
				Resulttype = TwitterSearchResultType.Mixed,
				SinceId = searchResult.Id,
				IncludeEntities = true
			});

			var search = currentTweets.Value.Statuses
				.Where(tw => tw.InReplyToStatusId == Convert.ToInt64(searchResult.Id)).ToList();

			return search;
		}

		private async Task<IEnumerable<TwitterStatus>> GetTwitterStatusesAsync(TwitterStatus searchResult)
		{
			var userTweets = await _twitterService.ListTweetsOnUserTimelineAsync(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = searchResult.User.ScreenName,
				Count = 100,
				IncludeRts = false,
				ExcludeReplies = false
			});

			var querySearch = userTweets.Value
				.Where(tw => tw.InReplyToStatusId == Convert.ToInt64(searchResult.Id))
				.ToList();

			return querySearch;
		}
	}
}
