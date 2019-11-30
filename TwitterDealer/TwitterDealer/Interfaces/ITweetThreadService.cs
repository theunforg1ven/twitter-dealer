using System.Threading.Tasks;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Interfaces
{
	public interface ITweetThreadService
	{
		Task<StatusTweet> GetUserThreadAsync(string tweetUrl);
	}
}
