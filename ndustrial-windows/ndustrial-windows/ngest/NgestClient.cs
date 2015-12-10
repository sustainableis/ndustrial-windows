


using System;
using System.Collections.Generic;
using com.ndustrialio.api.utils;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using com.ndustrialio.api.services;
using System.Reflection;

namespace com.ndustrialio.api.ngest
{
	public class NgestClient
	{

        private static String BASE_URL = "https://data.ndustrial.io/v1/";

		private String _feedKey, _feedToken, _feedTimeZone;

		private String _apiToken;

        private String _postURL;

        public NgestClient(String api_token, String feed_key)
		{
			_feedKey = feed_key;

            _apiToken = api_token;

			getFeedInfo();

            Console.WriteLine("Feed key: " + _feedKey);
            Console.WriteLine("Feed timezone: " + _feedTimeZone);
            Console.WriteLine("Feed token: " + _feedToken);

            // Construct post URL
            _postURL = BASE_URL
                + _feedToken
                + "/ngest/"
                + _feedKey;
        }

		private void getFeedInfo()
		{


			// Get API instance
			NdustrialIoApi api = new NdustrialIoApi(_apiToken);

            // Get info about our feed
			FeedData feedData =
				api.FEEDS.get(new Dictionary<String, String> { { "key", _feedKey } }) as FeedData;

            if (feedData == null)
            {
                throw new UnregisteredFeedException("Feed with key " + _feedKey + " not does not exist in the ndustrial.io system! Please register your feed.");
            }

            _feedToken = feedData.FeedToken;
            _feedTimeZone = feedData.TimeZone;
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

        public void sendDataAsync(TimeSeriesData data)
        {
            Thread thread = new Thread(() => sendData(data));
            thread.Start();
        }

    }
}
