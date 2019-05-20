using System;
using System.Runtime.Serialization;

namespace YoungMomsAssistant.UI.Infrastructure.Exceptions {
    class NotCreatedResponseException : SystemException {
        public NotCreatedResponseException() {
        }

        public NotCreatedResponseException(string message) : base(message) {
        }

        public NotCreatedResponseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected NotCreatedResponseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
