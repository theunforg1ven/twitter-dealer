using System.Collections.Generic;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Interfaces
{
	public interface IUserService
	{
		MainUserModel GetUserInfo(string screenName);

		IEnumerable<StatusTweet> GetUserTweets(string screenName);

		Task<IEnumerable<UserMedia>> GetUserMediaAsync(string screenName, int mediaCount);
	}
}
