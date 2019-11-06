using System;

namespace TwitterDealer.Data.Entities
{
	public class SavedThread
	{
		public int Id { get; set; }

		public bool IsFavourite { get; set; }

		public int RetweetCount { get; set; }

		public string TweetText { get; set; }

		public string Language { get; set; }

		public bool? IsPossiblySensitive { get; set; }

		public string Url { get; set; }

		public int FavoriteCount { get; set; }

		public DateTime? Created { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
