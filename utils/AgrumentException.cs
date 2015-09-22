

namespace com.ndustrialio.api.utils
{
    [Serializable()]
    public class ArgumentException : System.Exception
    {
        public ArgumentException() : base() { }
        public ArgumentException(string message) : base(message) { }
        public ArgumentException(string message, System.Exception inner) : base(message, inner) { }
    
        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected ArgumentException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    
    }   
}

