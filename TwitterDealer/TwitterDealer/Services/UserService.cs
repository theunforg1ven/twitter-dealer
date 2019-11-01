using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Interfaces;
using TwitterDealer.Models.BaseModels;
using TwitterDealer.Models.Enums;
using TwitterDealer.Models.TwitterUserModels;
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

		public MainUserModel GetUserInfo(string screenName)
		{
			var user = _twitterService.GetUserProfileFor(new GetUserProfileForOptions
			{ 
				ScreenName = screenName
			});

			var userModel = new MainUserModel
			{
				UserTwId = user.Id,
				FollowersCount = user.FollowersCount,
				UserTwName = user.Name,
				ImageUrl = user.ProfileImageUrl,
				Url = user.Url,
				IsProtected = user.IsProtected,
				ScreenName = user.ScreenName,
				Location = user.Location,
				FriendsCount = user.FriendsCount,
				ProfileBackgroundColor = user.ProfileBackgroundColor,
				ProfileTextColor = user.ProfileTextColor,
				ProfileLinkColor = user.ProfileLinkColor,
				ProfileBackgroundImageUrl = user.ProfileBackgroundImageUrl,
				FavouritesCount = user.FavouritesCount,
				ListedCount = user.ListedCount,
				StatusesCount = user.StatusesCount,
				IsProfileBackgroundTiled = user.IsProfileBackgroundTiled,
				IsVerified = user.IsVerified,
				IsGeoEnabled = user.IsGeoEnabled,
				Language = user.Language,
				CreatedDate = user.CreatedDate
			};

			return userModel;
		}

		public IEnumerable<StatusTweet> GetUserTweets(string screenName)
		{
			var currentTweets = _twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = screenName,
				Count = 100,
				IncludeRts = false,
				ExcludeReplies = true
			});

			var statusTweets = currentTweets
				.Select(tw => new StatusTweet
				{
					IsFavourite = tw.IsFavorited,
					RetweetCount = tw.RetweetCount,
					TweetText = tw.Text,
					Url = $"https://twitter.com/{screenName}/status/{tw.IdStr}",
					Language = tw.Language,
					IsPossiblySensitive = tw.IsPossiblySensitive,
					Created = tw.CreatedDate,
					MediaUrl = SelectMediaBase(tw)
				}); 

			return statusTweets;
		}

		public async Task<IEnumerable<UserMedia>> GetUserMediaAsync(string screenName, int mediaCount)
		{
			var media = await _twitterService.ListTweetsOnUserTimelineAsync(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = screenName,
				Count = 100,
				IncludeRts = false,
				ExcludeReplies = false
			});

			var querymedia = media.Value
							.Where(tw => tw.Entities.Media != null)
							.Select(tw => tw.Entities.Media)
							.SelectMany(m => m);

			var twMedia = SelectMedia(querymedia);

			return twMedia;
		}

		public IEnumerable<UserMedia> SelectMedia(IEnumerable<TwitterMedia> twMedia)
		{
			var media = twMedia.Select(m => new UserMedia
			{
				MediaUrl = m.MediaUrl,
				MediaType = SelectMediaType(m),
				TweetUrl = m.Url,
				TweetContent = null
			});

			return media;
		}

		public IEnumerable<BaseUserMedia> SelectMediaBase(TwitterStatus twStatus)
		{
			var media = twStatus.Entities.Media.Select(m => new BaseUserMedia
			{
				MediaUrl = m.MediaUrl,
				MediaType = SelectMediaType(m)
			});

			return media;
		}

		public TweetMediaType SelectMediaType(TwitterMedia twMedia) => twMedia.MediaType switch
		{
			TwitterMediaType.Photo => TweetMediaType.TweetImage,
			TwitterMediaType.Video => TweetMediaType.TweetVideo,
			TwitterMediaType.AnimatedGif => TweetMediaType.TweetGif,
			_ => TweetMediaType.TweetImage,
		};
	}
}
