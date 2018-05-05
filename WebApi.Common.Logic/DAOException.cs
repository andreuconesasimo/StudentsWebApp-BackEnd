using System;

namespace WebApi.Common.Logic
{
    public class DAOException : Exception
    {
        public DAOException() { }
        public DAOException(string message) : base(message) { }
        public DAOException(string message, Exception inner) : base(message, inner) { }
        protected DAOException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }    
}
