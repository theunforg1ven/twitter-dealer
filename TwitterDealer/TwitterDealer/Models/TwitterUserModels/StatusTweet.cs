using System;

namespace TwitterDealer.Models.TwitterUserModels
{
	public class StatusTweet
	{
		public bool IsFavourite { get; set; }

		public int RetweetCount { get; set; }

		public string TweetText { get; set; }

		public string Language { get; set; }

		public DateTime? Created { get; set; }
	}
}
