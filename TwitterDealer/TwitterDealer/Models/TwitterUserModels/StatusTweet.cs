using System;
using System.Collections.Generic;

namespace TwitterDealer.Models.TwitterUserModels
{
	public class StatusTweet
	{
		public bool IsFavourite { get; set; }

		public int RetweetCount { get; set; }

		public string TweetText { get; set; }

		public string Language { get; set; }

		public IEnumerable<UserMedia> MediaUrl { get; set; }

		public bool? IsPossiblySensitive { get; set; }

		public DateTime? Created { get; set; }
	}
}
