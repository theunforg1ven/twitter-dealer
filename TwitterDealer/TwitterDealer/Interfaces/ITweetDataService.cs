using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Interfaces
{
	public interface ITweetDataService
	{
		Task<IEnumerable<StatusTweet>> GetUserTweetsAsync(string tweetUrl);
	}
}
