using System;

namespace TwitterDealer.Models.TwitterUserModels
{
	public class MainUserModel
	{
		public long UserTwId { get; set; }

		public int FollowersCount { get; set; }

		public string UserTwName { get; set; }

		public string ImageUrl { get; set; }

		public string Url { get; set; }

		public bool? IsProtected { get; set; }

		public string ScreenName { get; set; }

		public string Location { get; set; }

		public int FriendsCount { get; set; }

		public string ProfileBackgroundColor { get; set; }

		public string ProfileTextColor { get; set; }

		public string ProfileLinkColor { get; set; }

		public string ProfileBackgroundImageUrl { get; set; }

		public int FavouritesCount { get; set; }

		public int ListedCount { get; set; }

		public int StatusesCount { get; set; }

		public bool IsProfileBackgroundTiled { get; set; }

		public bool? IsVerified { get; set; }

		public bool? IsGeoEnabled { get; set; }

		public string Language { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
