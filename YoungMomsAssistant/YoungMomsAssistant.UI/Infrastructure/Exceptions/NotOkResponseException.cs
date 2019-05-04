using System;
using System.Runtime.Serialization;

namespace YoungMomsAssistant.UI.Infrastructure.Exceptions {
    class NotOkResponseException : SystemException {
        public NotOkResponseException() {
        }

        public NotOkResponseException(string message) : base(message) {
        }

        public NotOkResponseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected NotOkResponseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
