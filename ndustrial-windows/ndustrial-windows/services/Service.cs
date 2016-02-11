
using System.Net;
using com.ndustrialio.api.errors;
using com.ndustrialio.api.http;

namespace com.ndustrialio.api.services
{

    // Base class for data returned from services
    public class ApiData {}


    public abstract class Service
    {
        private Client _client;

        public Service(Client client)
        {
            _client = client;
        }

        protected Response _get(Request req)
        {

            return performRequest(req.build(WebRequestMethods.Http.Get));
        }

        protected Response _put(Request req)
        {
            return performRequest(req.build(WebRequestMethods.Http.Put));
        }

        protected Response _post(Request req)
        {
            return performRequest(req.build(WebRequestMethods.Http.Post));
        }

        protected Response _delete(Request req)
        {
            // Microsoft does not have an enumeration for DELETE 
            return performRequest(req.build("DELETE"));
        }

        private Response performRequest(HttpWebRequest request)
        {
            try
            {
                return _client.execute(request);
            }
            catch (InvalidAcccessTokenException e)
            {
                if (_client.refreshEnabled())
                {
                    // Attempt token refresh
                    _client.refreshToken();

                    // We won't catch any exceptions here
                    return _client.execute(request);
                } else
                {
                    // Unable to refresh token
                    throw e;
                }
            }
        }

    }

}