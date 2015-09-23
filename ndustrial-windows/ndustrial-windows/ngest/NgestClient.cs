


using System;
using System.Collections.Generic;
using com.ndustrialio.api.utils;
using System.Net;
using System.Text;
using System.IO;

namespace com.ndustrialio.api.ngest
{
	public class NgestClient
	{

        private static String BASE_URL = "https://data.ndustrial.io/v1/";

		private String _feedKey, _feedToken, _feedTimeZone;
		
		private String _apiToken;

        private String _postURL;

        public NgestClient(string feed_key)
		{
			_feedKey = feed_key;
			
			getFeedInfo();

            // Construct post URL
            _postURL = BASE_URL
                + _feedToken
                + "/ngest/"
                + _feedKey;
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

        public void sendData(TimeSeriesData data)
        {
            // Convert all data to UTC
            data.delocalizeTimestamps(_feedTimeZone);

            List<String> dataToSend = data.getJSONData();

            foreach (var d in dataToSend)
            {
                // Set up post request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_postURL);

                request.Headers.Add("Content-type", "application/json");
                request.Headers.Add("Accept", "application/json");

                request.Method = WebRequestMethods.Http.Post;


                // Get string bytes
                UTF8Encoding encoding = new UTF8Encoding();

                // Write body data.. ridiculous API
                byte[] dataBytes = encoding.GetBytes(d);

                request.ContentLength = dataBytes.Length;

                Stream requestStream = request.GetRequestStream();

                requestStream.Write(dataBytes, 0, dataBytes.Length);

                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("Failed to send data!");
                }
            }


        }





    }
}

