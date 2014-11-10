// Kevin Dockx
//
// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

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
        public object AffectedObject { get; private set; }

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

        public JsonPatchException(Operation operation)
        {
            FailedOperation = operation;
        }

        public JsonPatchException(Operation operation, string message, object affectedObject)
        {
            FailedOperation = operation;
            _message = message;
            AffectedObject = affectedObject;
        }
    }


    public class JsonPatchException<T> : JsonPatchException where T : class
    {
        public new Operation<T> FailedOperation { get; private set; }
        public new T AffectedObject { get; private set; }

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
