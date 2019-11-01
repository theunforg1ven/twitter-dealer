using TwitterDealer.Models.Enums;

namespace TwitterDealer.Models.BaseModels
{
	public class BaseUserMedia
	{
		/// <summary>
		/// mediaUrl from "TwitterMedia" class
		/// </summary>
		public string MediaUrl { get; set; } // 

		/// <summary>
		/// 0 - image
		/// 1 - video
		/// 2 - gif
		/// if TwitterMediaType.Photo = 0, so we have: 0 - image
		/// </summary>
		public TweetMediaType? MediaType { get; set; }
	}
}
