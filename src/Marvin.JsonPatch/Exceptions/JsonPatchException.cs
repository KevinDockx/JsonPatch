// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Marvin.JsonPatch.Operations;
using System;

namespace Marvin.JsonPatch.Exceptions
{
    public class JsonPatchException : Exception
    {
        public OperationBase FailedOperation { get; private set; }
        public object AffectedObject { get; private set; }
        
        public int SuggestedStatusCode { get; internal set; }

        public JsonPatchException()
        {
        }

        public JsonPatchException(JsonPatchError jsonPatchError, Exception innerException, int suggestedStatusCode)
            : base(jsonPatchError.ErrorMessage, innerException)
        {
            FailedOperation = jsonPatchError.Operation;
            AffectedObject = jsonPatchError.AffectedObject;
            SuggestedStatusCode = suggestedStatusCode;
        }

        public JsonPatchException(JsonPatchError jsonPatchError, int suggestedStatusCode)
            : this(jsonPatchError, null, suggestedStatusCode)
        {
        }

        public JsonPatchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
