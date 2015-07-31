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


    //public class JsonPatchException : JsonPatchExceptionBase
    //{
    //    public Operation FailedOperation { get; private set; }
               
       
    //    public JsonPatchException()
    //    {

    //    }

    //    public JsonPatchException(string message):
    //        base(message, null)
    //    {

    //    }

    //    public JsonPatchException(Operation operation, string message, object affectedObject)
    //        : base(message, affectedObject)
    //    {
    //        FailedOperation = operation;        
    //    }

    //    public JsonPatchException(Operation operation, string message, object affectedObject, int statusCode)
    //        : this(operation, message, affectedObject)
    //    {
    //        StatusCode = statusCode;
    //    }

    //    public JsonPatchException(string message, Exception innerException, int statusCode)
    //        : base(message, innerException, statusCode)
    //    { 
    //    }

    //    //public JsonPatchException(Operation operation, string message, object affectedObject,
    //    //    int statusCode, Exception innerException)
    //    //    : this(operation, message, affectedObject, statusCode)
    //    //{
    //    //    InnerException = innerException;
    //    //}
    //}
}
