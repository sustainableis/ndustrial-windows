using com.ndustrialio.api.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace com.ndustrialio.api.services
{
    public class FeedType
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; }
    }

	public class FeedData : ApiData
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("facility_id")]
        public int FacilityID { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("key")]
        public String FeedKey { get; set; }

        //[JsonProperty("routing_keys")]
        //public List<String> RoutingKeys { get; set; }

        [JsonProperty("token")]
        public String FeedToken { get; set; }

        [JsonProperty("timezone")]
        public String TimeZone { get; set; }

        [JsonProperty("feed_type")]
        public FeedType FeedType { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

        [JsonProperty("created_at")]
        public String CreatedAt { get; set; }
    }


	public class Feeds : IService
	{
		public static String URI = "/feeds";
		
		private string _token;
		
		public Feeds(string token)
		{
			_token = token;
		}
		
		public ApiData get(Dictionary <String, String> args)
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

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Errore getting feed ! status: " + response.StatusCode);
            }

            String output = "";

            // Get response stream 
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                output = reader.ReadToEnd();
            }

            if (output.Length == 0)
            {
                return null;
            } else
            {
                // Convert to dictionary and return
                List<FeedData> jsonResult = JsonConvert.DeserializeObject<List<FeedData>>(output);

                return jsonResult[0];
            }
        }
	}
	
}