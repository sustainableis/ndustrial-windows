using com.ndustrialio.api.services;

namespace com.ndustrialio.api
{
	public class NdustrialIoApi
	{
	
		// Services
		public Service FEEDS;
		
		public NdustrialIoApi(string api_token)
		{
			FEEDS = new Feeds(api_token);
		}
		
	
	}	
}

