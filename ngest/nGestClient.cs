


namespace com.ndustrialio.api.ngest
{
	public class NgestClient
	{
	
		private string _feedKey, _feedToken, _feedTimeZone;
		
		private string _apiToken;
	
		public NgestClient(string feed_key)
		{
			_feedKey = feed_key;
			
			getFeedInfo()
			

		}
	
		private void getFeedInfo()
		{
			// Get API token
			string apiToken = Environment.GetEnvironmentVariable("API_TOKEN");
			
			// Get API instance
			ndustrialAPI api = new ndustrialAPI(apiToken);
			
			
		}
	
	
	}
}
