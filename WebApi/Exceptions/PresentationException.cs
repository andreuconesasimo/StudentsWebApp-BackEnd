using System;

namespace WebApi.Exceptions
{
    public class PresentationException:Exception
    {
        public PresentationException()
        {
        }

        public PresentationException(string message)
            : base(message)
        {
        }

        public PresentationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}