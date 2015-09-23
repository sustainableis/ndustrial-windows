


using System;
using System.Collections.Generic;
using com.ndustrialio.api.utils;

namespace com.ndustrialio.api.ngest
{
	public class NgestClient
	{

        private static String BASE_URL = "https://data.ndustrial.io/v1/";

		private String _feedKey, _feedToken, _feedTimeZone;
		
		private String _apiToken;
	
		public NgestClient(string feed_key)
		{
			_feedKey = feed_key;
			
			getFeedInfo();
			

		}
	
		private void getFeedInfo()
		{
			// Get API token
			string apiToken = Environment.GetEnvironmentVariable("API_TOKEN");
			
			// Get API instance
			ndustrialAPI api = new ndustrialAPI(apiToken);
			
            // Get info about our feed
			Dictionary<String, String> feedInfo = 
				api.FEEDS.get(new Dictionary<String, String> { { "key", _feedKey } });
			
            if (feedInfo.Count == 0)
            {
                throw new UnregisteredFeedException("Feed with key " + _feedKey + " not does not exist in the ndustrial.io system! Please register your feed.");
            }

            _feedToken = feedInfo["token"];
			_feedTimeZone = feedInfo["timezone"];
		}







    }
}

