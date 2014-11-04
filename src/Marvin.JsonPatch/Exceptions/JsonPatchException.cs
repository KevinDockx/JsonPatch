using Marvin.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marvin.JsonPatch.Exceptions
{
    public class JsonPatchException : Exception
    {
        public Operation FailedOperation { get; private set; }

        public JsonPatchException()
        {

        }

        public JsonPatchException(Operation operation)
        {
            FailedOperation = operation;
        }
    }


    public class JsonPatchException<T> : Exception where T: class
    {
        public Operation<T> FailedOperation { get; private set; }
        public T AffectedObject { get; private set; }

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

        public JsonPatchException(Operation<T> operation, string message, T affectedObject)
        {
            FailedOperation = operation;
            _message = message;
            AffectedObject = affectedObject; 
        }
    }
}
