

namespace com.ndustrialio.api.utils
{
	public static class RequestBuilder
	{
		public static string API_URL = "https://api.ndustrial.io/v1";
		
		private static string GET_VERB = "GET";
		private static string POST_VERB = "POST";
		
		public static HttpWebRequest GET(string url, string token, Dictionary<string, string> body = null)
		{
			HttpWebRequest ret = (HttpWebRequest)WebRequest.create(RequestBuilder.BASE_URL + url);
			
			Console.WriteLine(verb + ": " + RequestBuilder.BASE_URL + url)
			
			// Add authorization
			ret.Headers.Add("Authorization", "Bearer " + token);
			// Set verb
			ret.Method = RequestBuilder.GET_VERB;
			
			if (body != null)
			{
				// Add body somehow
			}
			
			return ret;
		}
		
		public static HttpWebRequest POST(string url)
		
	}	
}
