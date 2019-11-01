using TwitterDealer.Models.BaseModels;

namespace TwitterDealer.Models.TwitterUserModels
{
	public class UserMedia : BaseUserMedia
	{
		/// <summary>
		/// url from "TwitterMedia" class
		/// </summary>
		public string TweetUrl { get; set; }

		/// <summary>
		/// All content like text, language etc.
		/// </summary>
		public StatusTweet TweetContent { get; set; }
	}
}
