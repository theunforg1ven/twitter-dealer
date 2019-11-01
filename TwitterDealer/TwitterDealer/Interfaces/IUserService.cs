using System.Collections.Generic;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Interfaces
{
	public interface IUserService
	{
		Task<MainUserModel> GetUserInfoAsync(string screenName);

		Task<IEnumerable<StatusTweet>> GetUserTweetsAsync(string screenName);

		Task<IEnumerable<UserMedia>> GetUserMediaAsync(string screenName, int mediaCount);
	}
}
