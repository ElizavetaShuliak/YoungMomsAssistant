using System;
using System.Runtime.Serialization;

namespace YoungMomsAssistant.UI.Infrastructure.Exceptions {
    class AuthorizationException : SystemException {
        public AuthorizationException() {
        }

        public AuthorizationException(string message) : base(message) {
        }

        public AuthorizationException(string message, Exception innerException) : base(message, innerException) {
        }

        protected AuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
