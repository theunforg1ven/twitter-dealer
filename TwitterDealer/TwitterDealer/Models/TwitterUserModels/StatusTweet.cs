using System;
using System.Collections.Generic;
using TwitterDealer.Models.BaseModels;

namespace TwitterDealer.Models.TwitterUserModels
{
	public class StatusTweet
	{
		public bool IsFavourite { get; set; }

		public int RetweetCount { get; set; }

		public string TweetText { get; set; }

		public string Language { get; set; }

		public IEnumerable<BaseUserMedia> MediaUrl { get; set; }

		public bool? IsPossiblySensitive { get; set; }

		public string Url { get; set; }

		public DateTime? Created { get; set; }

		public IEnumerable<StatusTweet> Replies { get; set; }
	}
}
