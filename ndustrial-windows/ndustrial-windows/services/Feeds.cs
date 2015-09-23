using com.ndustrialio.api.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

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
		
		public Dictionary<String, String> get(Dictionary <String, String> args)
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

            // Get response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            String output = "";

            // Get response stream 
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                output = reader.ReadToEnd();
            }

            // Convert to dictionary and return
            return JsonConvert.DeserializeObject<Dictionary<String, String>>(output);
        }
	}
	
}