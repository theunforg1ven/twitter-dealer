using TwitterDealer.Models.Enums;

namespace TwitterDealer.Models.TwitterUserModels
{
	public class UserMedia
	{
		/// <summary>
		/// mediaUrl from "TwitterMedia" class
		/// </summary>
		public string MediaUrl { get; set; } // 

		/// <summary>
		/// url from "TwitterMedia" class
		/// </summary>
		public string TweetUrl { get; set; }

		/// <summary>
		/// 0 - image
		/// 1 - video
		/// 2 - gif
		/// if TwitterMediaType.Photo = 0, so we have: 0 - image
		/// </summary>
		public TweetMediaType MediaType { get; set; }

		/// <summary>
		/// All content like text, language etc.
		/// </summary>
		public StatusTweet TweetContent { get; set; }
	}
}
