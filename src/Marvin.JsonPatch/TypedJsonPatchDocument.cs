using Marvin.JsonPatch.Adapters;
using Marvin.JsonPatch.Helpers;
using Marvin.JsonPatch.Operations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


// Implementation details: the purpose of this type of patch document is to ensure we can do type-checking
// when producing a JsonPatchDocument.  However, we cannot send this "typed" over the wire, as that would require
// including type data in the JsonPatchDocument serialized as JSON (to allow for correct deserialization) - that's
// not according to RFC 6902, and would thus break cross-platform compatibility. 

namespace Marvin.JsonPatch
{

    public class JsonPatchDocument<T> where T:class
    {

        public List<Operation<T>> Operations { get; set; }
 
        public JsonPatchDocument()
        {
            Operations = new List<Operation<T>>();
        }

        /// <summary>
        /// Add operation.  Will result in, for example, 
        /// { "op": "add", "path": "/a/b/c", "value": [ "foo", "bar" ] }
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonPatchDocument<T> Add<TProp>(Expression<Func<T, TProp>> path, TProp value)
        {
            Operations.Add(new Operation<T>("add", ExpressionHelpers.GetPath<T, TProp>(path).ToLower(), null, value));
            return this;
        }

        public JsonPatchDocument<T> Add<TProp>(Expression<Func<T, IList<TProp>>> path, TProp value, int position)
        {
            Operations.Add(new Operation<T>("add", ExpressionHelpers.GetPath<T, IList<TProp>>(path).ToLower() + "/" + position, null, value));
            return this;
        }


        public JsonPatchDocument<T> Add<TProp>(Expression<Func<T, IList<TProp>>> path, TProp value)
        {
            Operations.Add(new Operation<T>("add", ExpressionHelpers.GetPath<T, IList<TProp>>(path).ToLower() + "/-", null, value));
            return this;
        }


        /// <summary>
        /// Remove value at target location.  Will result in, for example,
        /// { "op": "remove", "path": "/a/b/c" }
        /// </summary>
        /// <param name="remove"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public JsonPatchDocument<T> Remove<TProp>(Expression<Func<T, TProp>> path)
        {
            Operations.Add(new Operation<T>("remove", ExpressionHelpers.GetPath<T, TProp>(path).ToLower(), null));
            return this;
        }

        /// <summary>
        /// Replace value.  Will result in, for example,
        /// { "op": "replace", "path": "/a/b/c", "value": 42 }
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonPatchDocument<T> Replace<TProp>(Expression<Func<T, TProp>> path, TProp value)
        {
            Operations.Add(new Operation<T>("replace", ExpressionHelpers.GetPath<T, TProp>(path).ToLower(), null, value));
            return this;
        }


        /// <summary>
        /// Removes value at specified location and add it to the target location.  Will result in, for example:
        /// { "op": "move", "from": "/a/b/c", "path": "/a/b/d" }
        /// </summary>
        /// <param name="from"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public JsonPatchDocument<T> Move<TProp>(Expression<Func<T, TProp>> from, Expression<Func<T, TProp>> path)
        {
            Operations.Add(new Operation<T>("move", ExpressionHelpers.GetPath<T, TProp>(path).ToLower()
                , ExpressionHelpers.GetPath<T, TProp>(from).ToLower()));
            return this;
        }

        /// <summary>
        /// Copy the value at specified location to the target location.  Willr esult in, for example:
        /// { "op": "copy", "from": "/a/b/c", "path": "/a/b/e" }
        /// </summary>
        /// <param name="from"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public JsonPatchDocument<T> Copy<TProp>(Expression<Func<T, TProp>> from, Expression<Func<T, TProp>> path)
        {
            Operations.Add(new Operation<T>("copy", ExpressionHelpers.GetPath<T, TProp>(path).ToLower()
              , ExpressionHelpers.GetPath<T, TProp>(from).ToLower()));
            return this;
        }


        /// <summary>
        /// Tests that a value at the target location is equal to a specified value.  
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonPatchDocument<T> Test<TProp>(Expression<Func<T, TProp>> path, TProp value)
        {
            Operations.Add(new Operation<T>("test", ExpressionHelpers.GetPath<T, TProp>(path).ToLower()
              , null, value));
            return this;
        }


        public void ApplyTo(T objectToApplyTo)
        {
            ApplyTo(objectToApplyTo, new SimpleObjectAdapter<T>());
        }


        public void ApplyTo(T objectToApplyTo, IObjectAdapter<T> adapter)
        {

            // apply each operation in order
            foreach (var op in Operations)
            {
                op.Apply(objectToApplyTo, adapter);
            }

        }
 
    }
}
