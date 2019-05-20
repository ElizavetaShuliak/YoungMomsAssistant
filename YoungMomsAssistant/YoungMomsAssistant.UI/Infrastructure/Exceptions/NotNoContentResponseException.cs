using System;
using System.Runtime.Serialization;

namespace YoungMomsAssistant.UI.Infrastructure.Exceptions {
    class NotNoContentResponseException : SystemException {
        public NotNoContentResponseException() {
        }

        public NotNoContentResponseException(string message) : base(message) {
        }

        public NotNoContentResponseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected NotNoContentResponseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
