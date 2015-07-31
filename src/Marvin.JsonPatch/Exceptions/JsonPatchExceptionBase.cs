//// Kevin Dockx
////
//// Any comments, input: @KevinDockx
//// Any issues, requests: https://github.com/KevinDockx/JsonPatch
////
//// Enjoy :-)

//using Marvin.JsonPatch.Operations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Marvin.JsonPatch.Exceptions
//{
//    public abstract class JsonPatchExceptionBase : Exception
//    {
//        public new Exception InnerException { get; internal set; }

//        public int StatusCode { get; internal set; }

//        public object AffectedObject { get; internal set; }

//        private string _message = "";
//        public override string Message
//        {
//            get
//            {
//                return _message;
//            }

//        }

//        public JsonPatchExceptionBase()
//        {

//        }

//        public JsonPatchExceptionBase(string message, object affectedObject)
//        {
//            _message = message;
//            AffectedObject = affectedObject;
//         }

//        public JsonPatchExceptionBase(string message, Exception innerException)
//        {
//            _message = message;
//            InnerException = innerException;
//        }


//        public JsonPatchExceptionBase(string message, Exception innerException, int statusCode)
//            : this(message, innerException)
//        {
//            StatusCode = statusCode;
//        }

//    }


//}
