

namespace com.ndustrialio.api.utils
{
	public class RequestBuilder
	{
		public static string BASE_URL = "https://api.ndustrial.io/v1";
		
		public static string GET_VERB = "GET";
		
		private string _token, _url, _verb;
		
		private Dictionary<string, string> _body;
	
		
		private RequestBuilder(string verb, string url, string token)
		{
			_url = url;
			_token = token;
			_verb = verb;
			
			_body = new Dictionary<string, string>();
		}
		
		
		public setParameter(string key, string value)
		{
			urlKey = "{" + key + "}";
			
			// Replace url parameters if they exist
			if (_url.Contains(urkKey))
			{
				_url = _url.Replace(urkKey, value);
			} else
			{
				if (!_verb.Equals(RequestBuilder.GET_VERB))
				{
					// put in body
					_body.Add(key, value);
				}
			}
			
		}
		
		public HttpWebRequest getRequest()
		{
			HttpWebRequest ret = (HttpWebRequest)WebRequest.create(BASE_URL + _url);
			
			Console.WriteLine(verb + ": " + BASE_URL + _url)
			
			// Add authorization
			ret.Headers.Add("Authorization", "Bearer " + _token);
			// Set verb
			ret.Method = _verb;
			
			if (!verb.Equals(RequestBuilder.GET_VERB))
			{
				// Add body somehow
			}
			
			return ret;
		}
		
		public static RequestBuilder GET(string url, string token)
		{
			return new RequestBuilder(RequestBuilder.GET_VERB, url, token);
		}
		
	}	
}
