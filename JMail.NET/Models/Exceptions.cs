using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMail.NET.Models
{
    [Serializable]
    public class InvalidJMailRelayResponseException : Exception
    {
        public InvalidJMailRelayResponseException() : base() { }
        public InvalidJMailRelayResponseException(string message) : base(message) { }
        public InvalidJMailRelayResponseException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected InvalidJMailRelayResponseException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class TxtRecordNotFoundException : Exception
    {
        public TxtRecordNotFoundException() : base() { }
        public TxtRecordNotFoundException(string message) : base(message) { }
        public TxtRecordNotFoundException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected TxtRecordNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class InvalidJMailTxtRecordException : Exception
    {
        public InvalidJMailTxtRecordException() : base() { }
        public InvalidJMailTxtRecordException(string message) : base(message) { }
        public InvalidJMailTxtRecordException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected InvalidJMailTxtRecordException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class UnauthorizedJMailSenderException : Exception
    {
        public UnauthorizedJMailSenderException() : base() { }
        public UnauthorizedJMailSenderException(string message) : base(message) { }
        public UnauthorizedJMailSenderException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected UnauthorizedJMailSenderException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class InvalidJMailAddressException : Exception
    {
        public InvalidJMailAddressException() : base() { }
        public InvalidJMailAddressException(string message) : base(message) { }
        public InvalidJMailAddressException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected InvalidJMailAddressException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
