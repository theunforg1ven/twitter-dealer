using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterDealer.Interfaces;
using TwitterDealer.TwitterApi;

namespace TwitterDealer.Services
{
	public class TweetThreadService : ITweetThreadService
	{
		private readonly TwitterService _twitterService;

		public TweetThreadService()
		{
			_twitterService = AuthInit.TwitterService;
		}
	}
}
