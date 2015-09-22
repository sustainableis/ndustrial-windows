
namespace com.ndustrialio.api.services
{
	
	public class Feeds : IService
	{
		public static String URL = "/feeds/{feed_id}";
		
		private string _token;
		
		public Feeds(string token)
		{
			_token = token;
		}
		
		public Dictionary<String, String> get(Dictionary <string, string> args)
		{
			if (!args.ContainsKey("feed_id"))
			{
				throw new ArgumentException("feed_id is necessary for feeds.get!");
			}
			
			// Build request
			RequestBuilder bulder = RequestBuilder.GET(Feeds.URL, RequestBuilder._token);
		
			foreach (var key in args.Keys)
			{
				builder.setParameter(key, args[key]);
			}
			
			// Get request
			HttpWebRequest = builder.getRequest();
			
			
		}
	}
	
}