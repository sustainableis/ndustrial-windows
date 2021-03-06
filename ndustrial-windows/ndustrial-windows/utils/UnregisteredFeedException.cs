

using System;

namespace com.ndustrialio.api.utils
{
    [Serializable()]
    public class UnregisteredFeedException : System.Exception
    {
        public UnregisteredFeedException() : base() { }
        public UnregisteredFeedException(string message) : base(message) { }
        public UnregisteredFeedException(string message, System.Exception inner) : base(message, inner) { }
    
        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected UnregisteredFeedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    
    }   
}

