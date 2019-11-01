using System.Collections.Generic;
using TweetSharp;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Interfaces
{
	public interface IUserService
	{
		MainUserModel GetUserInfo(string screenName);

		IEnumerable<StatusTweet> GetUserTweets(string screenName);

		IEnumerable<TwitterMedia> GetUserMedia(string screenName, int mediaCount);
	}
}
