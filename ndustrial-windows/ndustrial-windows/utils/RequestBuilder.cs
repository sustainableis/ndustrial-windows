

using System;
using System.Collections.Generic;
using System.Net;

namespace com.ndustrialio.api.utils
{
	public static class RequestBuilder
	{
		public static string BASE_URL = "https://api.ndustrial.io/v1";
		
		public static HttpWebRequest GET(string url, string token, Dictionary<String, String> body = null)
		{
			HttpWebRequest ret = (HttpWebRequest)WebRequest.Create(RequestBuilder.BASE_URL + url);

            //Console.WriteLine(WebRequestMethods.Http.Get + ": " + RequestBuilder.BASE_URL + url);
			
			// Add authorization
			ret.Headers.Add("Authorization", "Bearer " + token);
            // Set verb
            ret.Method = WebRequestMethods.Http.Get;
			
			if (body != null)
			{
				// Add body somehow
			}
			
			return ret;
		}
	}	
}
