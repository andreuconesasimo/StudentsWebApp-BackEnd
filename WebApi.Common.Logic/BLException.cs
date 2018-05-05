using System;

namespace WebApi.Common.Logic
{
    public class BLException : Exception
    {
        public BLException() { }
        public BLException(string message) : base(message) { }
        public BLException(string message, Exception inner) : base(message, inner) { }
        protected BLException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }    
}
