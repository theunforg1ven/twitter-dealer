using System.Collections.Generic;
using System.Linq;
using TweetSharp;
using TwitterDealer.Models.BaseModels;
using TwitterDealer.Models.Enums;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Helpers
{
	public class SelectMediaHelper
	{
		public IEnumerable<UserMedia> SelectMedia(IEnumerable<TwitterExtendedEntity> twMedia)
		{
			var media = twMedia.Select(m => new UserMedia
			{
				MediaUrl = m.MediaUrl.ToString(),
				MediaType = SelectMediaType(m),
				TweetUrl = m.Url.ToString(),
				TweetContent = null
			});

			return media;
		}

		public IEnumerable<BaseUserMedia> SelectMediaBase(TwitterStatus twStatus)
		{
			if (twStatus.ExtendedEntities == null)
				return null;

			var media = twStatus.ExtendedEntities.Media.Select(m => new BaseUserMedia
			{
				MediaUrl = m.MediaUrl.ToString(),
				MediaType = SelectMediaType(m)
			});


			return media;
		}

		private TweetMediaType SelectMediaType(TwitterExtendedEntity twMedia) => twMedia.ExtendedEntityType switch
		{
			TwitterMediaType.Photo => TweetMediaType.TweetImage,
			TwitterMediaType.Video => TweetMediaType.TweetVideo,
			TwitterMediaType.AnimatedGif => TweetMediaType.TweetGif,
			_ => TweetMediaType.None,
		};
	}
}
