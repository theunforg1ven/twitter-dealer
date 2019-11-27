using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Helpers;
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

		private readonly SelectMediaHelper _sMediaHelper;
		public UserService()
		{
			_twitterService = AuthInit.TwitterService;
			_sMediaHelper = new SelectMediaHelper();
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
			// TODO: ExcludeReplies = change because of boolean value from frontend
			var currentTweets = await _twitterService.ListTweetsOnUserTimelineAsync(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = screenName,
				Count = 100,
				IncludeRts = false,
				ExcludeReplies = false
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
					MediaUrl = _sMediaHelper.SelectMediaBase(tw)
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
							.Where(tw => tw.ExtendedEntities != null)
							.Select(tw => tw.ExtendedEntities.Media)
							.SelectMany(m => m);

			var twMedia = _sMediaHelper.SelectMedia(querymedia);

			return twMedia;
		}
	}
}
