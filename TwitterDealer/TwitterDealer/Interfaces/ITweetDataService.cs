using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterDealer.Interfaces
{
	public interface ITweetDataService
	{
		Task<TwitterStatus> GetUserTweetsAsync(string tweetUrl);
	}
}
