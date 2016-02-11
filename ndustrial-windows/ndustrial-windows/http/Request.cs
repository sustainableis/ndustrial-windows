using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace com.ndustrialio.api.http
{
    public class Request
    {

        public static string BASE_URL = "https://api.ndustrial.io/v1";


        public string _uri;
        public Dictionary<String, String> _body;

        public Dictionary<String, String> _params;

        public Request(String uri, Dictionary<String, String> parameters=null, Dictionary<String, String> body=null)
        {
            _uri = uri;
            _body = body;
            _params = parameters;
        }

        public HttpWebRequest build(String verb)
        {
            // Build query string
            StringBuilder sb = new StringBuilder(Request.BASE_URL);

            // Add URI extension
            sb.Append(_uri);

            if (_params != null && _params.Count > 0)
            {
                sb.Append("?");

                sb.Append(urlEncode(_params));
            }

            HttpWebRequest ret = (HttpWebRequest)WebRequest.Create(sb.ToString());

            // Set verb
            ret.Method = verb;

            // Handle body
            if (_body != null)
            {
                ret.ContentType = "application/x-www-form-urlencoded";

                ASCIIEncoding encoding = new ASCIIEncoding();

                byte[] bodyBytes = encoding.GetBytes(urlEncode(_body));

                ret.ContentLength = bodyBytes.Length;

                Stream s = ret.GetRequestStream();

                s.Write(bodyBytes, 0, bodyBytes.Length);

                s.Close();
            }

            return ret;
        }

        private String urlEncode(Dictionary<String, String> args)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<String, String> entry in args)
            {
                sb.Append(entry.Key + '=' + entry.Value);
                sb.Append("&");
            }

            // Remove trailing "&"
            sb.Length--;

            return sb.ToString();
        }
    }



}
