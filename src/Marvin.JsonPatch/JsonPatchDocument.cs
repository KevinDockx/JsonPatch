 //Kevin Dockx

 //Any comments, input: @KevinDockx
 //Any issues, requests: https://github.com/KevinDockx/JsonPatch

 //Enjoy :-)
  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marvin.JsonPatch.Operations;
using Marvin.JsonPatch.Adapters;

namespace Marvin.JsonPatch
{
    /// <summary>
    /// Implementation of the JSON Patch Document to allow patching (partial updates) - this document class 
    /// can be used to create a set of operations on the consumer side that track the operations that have to be applied
    /// on the server side.
    /// 
    /// It's an implementation of IETF RFC6902, JSON Patch
    /// 
    /// For reference: http://tools.ietf.org/html/rfc6902
    /// 
    /// "JSON Patch defines a JSON document structure for expressing a
    ///  sequence of operations to apply to a JavaScript Object Notation
    ///  (JSON) document; it is suitable for use with the HTTP PATCH method.
    ///  The "application/json-patch+json" media type is used to identify such
    ///  patch documents."
    /// </summary>
    public class JsonPatchDocument
    {
        public List<Operation> Operations { get; set; }


        public JsonPatchDocument()
        {
            Operations = new List<Operation>();
        }

        /// <summary>
        /// Add operation.  Will result in, for example, 
        /// { "op": "add", "path": "/a/b/c", "value": [ "foo", "bar" ] }
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonPatchDocument Add(string path, object value)
        {
            Operations.Add(new Operation("add", path, null, value));
            return this;
        }


        /// <summary>
        /// Remove value at target location.  Will result in, for example,
        /// { "op": "remove", "path": "/a/b/c" }
        /// </summary>
        /// <param name="remove"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public JsonPatchDocument Remove(string path)
        {
            Operations.Add(new Operation("remove", path, null,null));
            return this;
        }

        /// <summary>
        /// Replace value.  Will result in, for example,
        /// { "op": "replace", "path": "/a/b/c", "value": 42 }
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonPatchDocument Replace(string path, object value)
        {
            Operations.Add(new Operation("replace", path, null, value));
            return this;
        }


        /// <summary>
        /// Removes value at specified location and add it to the target location.  Will result in, for example:
        /// { "op": "move", "from": "/a/b/c", "path": "/a/b/d" }
        /// </summary>
        /// <param name="from"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public JsonPatchDocument Move(string from, string path)
        {
            Operations.Add(new Operation("move", path, from, null));
            return this;
        }

        /// <summary>
        /// Copy the value at specified location to the target location.  Willr esult in, for example:
        /// { "op": "copy", "from": "/a/b/c", "path": "/a/b/e" }
        /// </summary>
        /// <param name="from"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public JsonPatchDocument Copy(string from, string path)
        {
            Operations.Add(new Operation("copy", path, from, null));
            return this;
        }


        ///// <summary>
        ///// Tests that a value at the target location is equal to a specified value.  
        ///// Currently not implemented!
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public JsonPatchDocument Test(string path, object value)
        //{
        //    throw new NotImplementedException();
        //}



        public void ApplyTo<T>(T objectToApplyTo, IDynamicObjectAdapter<T> adapter) where T: class
        {

            // apply each operation in order
            foreach (var op in Operations)
            {
                op.Apply<T>(objectToApplyTo, adapter);
            }

        }

    }
}
