using System.Threading.Tasks;
using TwitterDealer.Models.TwitterUserModels;

namespace TwitterDealer.Interfaces
{
	public interface ISaveThreadRepository
	{
		Task<bool> AddThreadAsync(StatusTweet stTweet);
	}
}
