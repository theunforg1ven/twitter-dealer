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

		public async Task<MainUserModel> GetUserInfoAsync(string screenName)
		{
			var user = await _twitterService.GetUserProfileForAsync(new GetUserProfileForOptions
			{ 
				ScreenName = screenName
			});

			var userModel = new MainUserModel
			{
				UserTwId = user.Value.Id,
				FollowersCount = user.Value.FollowersCount,
				UserTwName = user.Value.Name,
				ImageUrl = user.Value.ProfileImageUrl,
				Url = user.Value.Url,
				IsProtected = user.Value.IsProtected,
				ScreenName = user.Value.ScreenName,
				Location = user.Value.Location,
				FriendsCount = user.Value.FriendsCount,
				ProfileBackgroundColor = user.Value.ProfileBackgroundColor,
				ProfileTextColor = user.Value.ProfileTextColor,
				ProfileLinkColor = user.Value.ProfileLinkColor,
				ProfileBackgroundImageUrl = user.Value.ProfileBackgroundImageUrl,
				FavouritesCount = user.Value.FavouritesCount,
				ListedCount = user.Value.ListedCount,
				StatusesCount = user.Value.StatusesCount,
				IsProfileBackgroundTiled = user.Value.IsProfileBackgroundTiled,
				IsVerified = user.Value.IsVerified,
				IsGeoEnabled = user.Value.IsGeoEnabled,
				Language = user.Value.Language,
				CreatedDate = user.Value.CreatedDate
			};

			return userModel;
		}

		public async Task<IEnumerable<StatusTweet>> GetUserTweetsAsync(string screenName)
		{
			var currentTweets = await _twitterService.ListTweetsOnUserTimelineAsync(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = screenName,
				Count = 100,
				IncludeRts = false,
				ExcludeReplies = true
			});

			var statusTweets = currentTweets.Value
				.Select(tw => new StatusTweet
				{
					IsFavourite = tw.IsFavorited,
					FavoriteCount = tw.FavoriteCount,
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

		private IEnumerable<UserMedia> SelectMedia(IEnumerable<TwitterMedia> twMedia)
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

		private IEnumerable<BaseUserMedia> SelectMediaBase(TwitterStatus twStatus)
		{
			var media = twStatus.Entities.Media.Select(m => new BaseUserMedia
			{
				MediaUrl = m.MediaUrl,
				MediaType = SelectMediaType(m)
			});

			return media;
		}

		private TweetMediaType SelectMediaType(TwitterMedia twMedia) => twMedia.MediaType switch
		{
			TwitterMediaType.Photo => TweetMediaType.TweetImage,
			TwitterMediaType.Video => TweetMediaType.TweetVideo,
			TwitterMediaType.AnimatedGif => TweetMediaType.TweetGif,
			_ => TweetMediaType.None,
		};
	}
}
