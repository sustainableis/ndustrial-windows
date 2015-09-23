using com.ndustrialio.api.utils.RequestBuilder;


namespace com.ndustrialio.api.services
{
	
	public class Feeds : IService
	{
		public static String URI = "/feeds";
		
		private string _token;
		
		public Feeds(string token)
		{
			_token = token;
		}
		
		public Dictionary<String, String> get(Dictionary <string, string> args)
		{
			string uri = Feeds.URI;
			
			if (args.ContainsKey("id"))
			{
				uri += "/";
				uri += args["id"];
			} else if (args.ContainsKey("key"))
			{
				uri += "?key=";
				uri += args["key"];
			}
		

			
			// Get request
			HttpWebRequest request = RequestBuilder.GET(uri, _token, null);
			
			
			
			
		}
	}
	
}