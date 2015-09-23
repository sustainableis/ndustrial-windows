using com.ndustrialio.api.services;

namespace com.ndustrialio.api
{
	public class ndustrialAPI
	{
	
		// Services
		public IService FEEDS;
		
		public ndustrialAPI(string api_token)
		{
			FEEDS = new Feeds(api_token);
		}
		
	
	}	
}

