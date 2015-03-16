using Marvin.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.JsonPatch.Exceptions
{
    public class JsonPatchException: JsonPatchExceptionBase 
    {
        public Operation  FailedOperation { get; private set; }
        public new object AffectedObject { get; private set; }

        private string _message = "";
        public override string Message
        {
            get
            {
                return _message;
            }

        }

        public JsonPatchException()
        {

        }

        public JsonPatchException(Operation operation, string message, object affectedObject)
        {
            FailedOperation = operation;
            _message = message;
            AffectedObject = affectedObject;
        }

        public JsonPatchException(Operation operation, string message, object affectedObject, int statusCode)
            : this(operation, message, affectedObject)
        {
            StatusCode = statusCode;
        }

        public JsonPatchException(Operation operation, string message, object affectedObject,
            int statusCode, Exception innerException)
            : this(operation, message, affectedObject, statusCode)
        {
            InnerException = innerException;
        }
    }
}
