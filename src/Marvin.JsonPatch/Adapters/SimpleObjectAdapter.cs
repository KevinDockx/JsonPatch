using Marvin.JsonPatch.Exceptions;
using Marvin.JsonPatch.Helpers;
using Marvin.JsonPatch.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Marvin.JsonPatch.Adapters
{
    public class SimpleObjectAdapter<T> : Marvin.JsonPatch.Adapters.IObjectAdapter<T> where T : class
    {


        /// <summary>
        /// The "add" operation performs one of the following functions,
        /// depending upon what the target location references:
        /// 
        /// o  If the target location specifies an array index, a new value is
        ///    inserted into the array at the specified index.
        /// 
        /// o  If the target location specifies an object member that does not
        ///    already exist, a new member is added to the object.
        /// 
        /// o  If the target location specifies an object member that does exist,
        ///    that member's value is replaced.
        /// 
        /// The operation object MUST contain a "value" member whose content
        /// specifies the value to be added.
        /// 
        /// For example:
        /// 
        /// { "op": "add", "path": "/a/b/c", "value": [ "foo", "bar" ] }
        /// 
        /// When the operation is applied, the target location MUST reference one
        /// of:
        /// 
        /// o  The root of the target document - whereupon the specified value
        ///    becomes the entire content of the target document.
        /// 
        /// o  A member to add to an existing object - whereupon the supplied
        ///    value is added to that object at the indicated location.  If the
        ///    member already exists, it is replaced by the specified value.
        /// 
        /// o  An element to add to an existing array - whereupon the supplied
        ///    value is added to the array at the indicated location.  Any
        ///    elements at or above the specified index are shifted one position
        ///    to the right.  The specified index MUST NOT be greater than the
        ///    number of elements in the array.  If the "-" character is used to
        ///    index the end of the array (see [RFC6901]), this has the effect of
        ///    appending the value to the array.
        /// 
        /// Because this operation is designed to add to existing objects and
        /// arrays, its target location will often not exist.  Although the
        /// pointer's error handling algorithm will thus be invoked, this
        /// specification defines the error handling behavior for "add" pointers
        /// to ignore that error and add the value as specified.
        /// 
        /// However, the object itself or an array containing it does need to
        /// exist, and it remains an error for that not to be the case.  For
        /// example, an "add" with a target location of "/a/b" starting with this
        /// document:
        /// 
        /// { "a": { "foo": 1 } }
        /// 
        /// is not an error, because "a" exists, and "b" will be added to its
        /// value.  It is an error in this document:
        /// 
        /// { "q": { "bar": 2 } }
        /// 
        /// because "a" does not exist.
        /// </summary>
        /// <param name="operation">The add operation</param>
        /// <param name="objectApplyTo">Object to apply the operation to</param>
        public void Add(Operation<T> operation, T objectToApplyTo)
        {
            // add, in our implementation, does not just "add" properties - that's
            // technically impossible;  It can however be used to add items to arrays,
            // or to replace values.

            // first up: if the path ends in a numeric value, we're inserting in a list and
            // that value represents the position; if the path ends in "-", we're appending
            // to the list.

            bool appendList = false;
            int positionAsInteger = -1;
            string actualPathToProperty = operation.path;
            
            if (operation.path.EndsWith("/-"))
            {
                appendList = true;
                actualPathToProperty = operation.path.Substring(0, operation.path.Length-2);
            }
            else
            {
                 positionAsInteger = PropertyHelpers.GetNumericEnd(operation.path);

                if (positionAsInteger > -1)
                {
                    actualPathToProperty = operation.path.Substring(0,
                        operation.path.IndexOf('/' + positionAsInteger.ToString()));
                }
            }

            // does property at path exist?
            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, actualPathToProperty)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location path: {0} does not exist", operation.path),
                    objectToApplyTo);
            }

            // it exists.  If it' an array, add to that array.  If it's not, we replace.

            // get the property path
            PropertyInfo pathProperty = PropertyHelpers.FindProperty(objectToApplyTo, actualPathToProperty);

            // is the path an array (but not a string (= char[]))?  In this case,
            // the path must end with "/position" or "/-", which we already determined before.


            if (appendList || positionAsInteger > -1)
            {

                var isNonStringArray = !(pathProperty.PropertyType == typeof(string))
                    && typeof(IList).IsAssignableFrom(pathProperty.PropertyType);

                // what if it's an array but there's no position??
                if (isNonStringArray)
                {
                    // now, get the generic type of the enumerable
                    var genericTypeOfArray = PropertyHelpers.GetEnumerableType(pathProperty.PropertyType);

                    // check if the value can be cast to that type
                    if (!(PropertyHelpers.CheckIfValueCanBeCast(genericTypeOfArray, operation.value)))
                    {
                        throw new JsonPatchException<T>(operation,
                          string.Format("Patch failed: provided value is invalid for array property type at location path: {0}",
                          operation.path),
                          objectToApplyTo);
                    }


                    // get value (it can be cast, we just checked that)
                    var array = PropertyHelpers.GetValue(pathProperty, objectToApplyTo) as IList;

                    if (appendList)
                    {
                        array.Add(operation.value);
                    }
                    else
                    {
                        if (positionAsInteger < array.Count)
                        {
                            array.Insert(positionAsInteger, operation.value);
                        }
                        else
                        {
                            throw new JsonPatchException<T>(operation,
                       string.Format("Patch failed: provided path is invalid for array property type at location path: {0}: position larger than array size",
                       operation.path),
                       objectToApplyTo);
                        }
                    }
                                 
                }
                else
                {
                    throw new JsonPatchException<T>(operation,
                       string.Format("Patch failed: provided path is invalid for array property type at location path: {0}: expected array",
                       operation.path),
                       objectToApplyTo);
                }
            }
            else
            {
                if (!(PropertyHelpers.CheckIfValueCanBeCast(pathProperty.PropertyType, operation.value)))
                {
                    throw new JsonPatchException<T>(operation,
                      string.Format("Patch failed: provided value is invalid for property type at location path: {0}",
                      operation.path),
                      objectToApplyTo);
                }

                // set the new value
                PropertyHelpers.SetValue(pathProperty, objectToApplyTo, operation.value);
            }
        }


        /// <summary>
        /// The "move" operation removes the value at a specified location and
        /// adds it to the target location.
        /// 
        /// The operation object MUST contain a "from" member, which is a string
        /// containing a JSON Pointer value that references the location in the
        /// target document to move the value from.
        /// 
        /// The "from" location MUST exist for the operation to be successful.
        /// 
        /// For example:
        /// 
        /// { "op": "move", "from": "/a/b/c", "path": "/a/b/d" }
        /// 
        /// This operation is functionally identical to a "remove" operation on
        /// the "from" location, followed immediately by an "add" operation at
        /// the target location with the value that was just removed.
        /// 
        /// The "from" location MUST NOT be a proper prefix of the "path"
        /// location; i.e., a location cannot be moved into one of its children.
        /// </summary>
        /// <param name="operation">The move operation</param>
        /// <param name="objectApplyTo">Object to apply the operation to</param>
        public void Move(Operation<T> operation, T objectToApplyTo)
        {
            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, operation.from)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location from: {0} does not exist", operation.from),
                    objectToApplyTo);
            }


            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, operation.path)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location path: {0} does not exist", operation.path),
                    objectToApplyTo);
            }

            // are both properties of the same type?

            PropertyInfo fromProperty = PropertyHelpers.FindProperty(objectToApplyTo, operation.from);
            PropertyInfo pathProperty = PropertyHelpers.FindProperty(objectToApplyTo, operation.path);

            if (!(fromProperty.PropertyType == pathProperty.PropertyType))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Path failed: property at path {0} has a different type than property at from {1}",
                    operation.path, operation.from),
                    objectToApplyTo);
            }

            // if it exists, and it's of the same type as the "from" property, 
            // copy over the value of "from" to "path"

            PropertyHelpers.CopyValue(objectToApplyTo, fromProperty, pathProperty);

            // remove the value at the "from" location
            PropertyHelpers.SetValue(fromProperty, objectToApplyTo, null);

        }


        /// <summary>
        /// The "remove" operation removes the value at the target location.
        ///
        /// The target location MUST exist for the operation to be successful.
        /// 
        /// For example:
        /// 
        /// { "op": "remove", "path": "/a/b/c" }
        /// 
        /// If removing an element from an array, any elements above the
        /// specified index are shifted one position to the left.
        /// </summary>
        /// <param name="operation">The remove operation</param>
        /// <param name="objectApplyTo">Object to apply the operation to</param>
        public void Remove(Operation<T> operation, T objectToApplyTo)
        {
            // does the target location exist?
            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, operation.path)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location path: {0} does not exist", operation.path),
                    objectToApplyTo);
            }

            // get the property, and remove it - in this case, for DTO's, that means setting
            // it to null or its default value.
            PropertyInfo pathProperty = PropertyHelpers.FindProperty(objectToApplyTo, operation.path);

            // setting the value to "null" will use the default value in case of value types, and
            // null in case of reference types
            PropertyHelpers.SetValue(pathProperty, objectToApplyTo, null);
        }



        /// <summary>
        /// The "test" operation tests that a value at the target location is
        /// equal to a specified value.
        /// 
        /// The operation object MUST contain a "value" member that conveys the
        /// value to be compared to the target location's value.
        /// 
        /// The target location MUST be equal to the "value" value for the
        /// operation to be considered successful.
        /// 
        /// Here, "equal" means that the value at the target location and the
        /// value conveyed by "value" are of the same JSON type, and that they
        /// are considered equal by the following rules for that type:
        /// 
        /// o  strings: are considered equal if they contain the same number of
        ///    Unicode characters and their code points are byte-by-byte equal.
        /// 
        /// o  numbers: are considered equal if their values are numerically
        ///    equal.
        /// 
        /// o  arrays: are considered equal if they contain the same number of
        ///    values, and if each value can be considered equal to the value at
        ///    the corresponding position in the other array, using this list of
        ///    type-specific rules.
        ///
        /// o  objects: are considered equal if they contain the same number of
        ///    members, and if each member can be considered equal to a member in
        ///    the other object, by comparing their keys (as strings) and their
        ///    values (using this list of type-specific rules).
        ///
        /// o  literals (false, true, and null): are considered equal if they are
        ///    the same.
        ///
        /// Note that the comparison that is done is a logical comparison; e.g.,
        /// whitespace between the member values of an array is not significant.
        ///
        /// Also, note that ordering of the serialization of object members is
        /// not significant.
        ///
        /// For example:
        ///
        /// { "op": "test", "path": "/a/b/c", "value": "foo" }
        /// </summary>
        /// <param name="operation">The test operation</param>
        /// <param name="objectApplyTo">Object to apply the operation to</param>
        public void Test(Operation<T> operation, T objectToApplyTo)
        {
            // does the target location exist?
            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, operation.path)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location path: {0} does not exist", operation.path),
                    objectToApplyTo);
            }

            // get the type of the property, and check if the value can be casted to that type.
            PropertyInfo pathProperty = PropertyHelpers.FindProperty(objectToApplyTo, operation.path);

            if (!(PropertyHelpers.CheckIfValueCanBeCast(pathProperty.PropertyType, operation.value)))
            {
                throw new JsonPatchException<T>(operation,
                  string.Format("Patch failed: provided value is invalid for property type at location path: {0}", 
                  operation.path),
                  objectToApplyTo);
            }

            // do the test
            var valueToTest = PropertyHelpers.GetValue(pathProperty, objectToApplyTo);

            if (!(valueToTest.Equals(operation.value)))
            {
                throw new JsonPatchException<T>(operation,
                 string.Format("Patch failed: provided value at target location path: {0} does not match value to test against.",
                 operation.path),
                 objectToApplyTo);
            }

        }


        /// <summary>
        /// The "replace" operation replaces the value at the target location
        /// with a new value.  The operation object MUST contain a "value" member
        /// whose content specifies the replacement value.
        /// 
        /// The target location MUST exist for the operation to be successful.
        /// 
        /// For example:
        /// 
        /// { "op": "replace", "path": "/a/b/c", "value": 42 }
        /// 
        /// This operation is functionally identical to a "remove" operation for
        /// a value, followed immediately by an "add" operation at the same
        /// location with the replacement value.
        /// 
        /// Note: even though it's the same functionally, we do not call remove + add 
        /// for performance reasons (multiple checks of same requirements).
        /// </summary>
        /// <param name="operation">The replace operation</param>
        /// <param name="objectApplyTo">Object to apply the operation to</param>
        public void Replace(Operation<T> operation, T objectToApplyTo)
        {
            // does property at path exist?
            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, operation.path)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location path: {0} does not exist", operation.path),
                    objectToApplyTo);
            }

            // get the type of the property, and check if the value can be casted to that type.
            PropertyInfo pathProperty = PropertyHelpers.FindProperty(objectToApplyTo, operation.path);

            if (!(PropertyHelpers.CheckIfValueCanBeCast(pathProperty.PropertyType, operation.value)))
            {
                throw new JsonPatchException<T>(operation,
                  string.Format("Patch failed: provided value is invalid for property type at location path: {0}",
                  operation.path),
                  objectToApplyTo);
            }

            // set the new value
            PropertyHelpers.SetValue(pathProperty, objectToApplyTo, operation.value);

        }


        /// <summary>
        ///  The "copy" operation copies the value at a specified location to the
        ///  target location.
        ///  
        ///  The operation object MUST contain a "from" member, which is a string
        ///  containing a JSON Pointer value that references the location in the
        ///  target document to copy the value from.
        ///  
        ///  The "from" location MUST exist for the operation to be successful.
        ///  
        ///  For example:
        ///  
        ///  { "op": "copy", "from": "/a/b/c", "path": "/a/b/e" }
        ///  
        ///  This operation is functionally identical to an "add" operation at the
        ///  target location using the value specified in the "from" member.
        ///  
        ///  Note: even though it's the same functionally, we do not call add with 
        ///  the value specified in from for performance reasons (multiple checks of same requirements).
        /// </summary>
        /// <param name="operation">The copy operation</param>
        /// <param name="objectApplyTo">Object to apply the operation to</param>
        public void Copy(Operation<T> operation, T objectToApplyTo)
        {
            // note: this get executed at the API side.  However, there's nothing that guarantees that
            // the object we're working on on the client is the exact same as the one at the server
            // (eg: same namespace & classname, different class implementation) - therefore, we execute all checks.


            // find the property in "from" on T

            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, operation.from)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location from: {0} does not exist", operation.from),
                    objectToApplyTo);
            }

            // find the property in "path" on T - as we're working on a typed object, this MUST exist;
            // for untyped operations, a copy operation will add the field dynamically if needed.

            if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, operation.path)))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Patch failed: property at location path: {0} does not exist", operation.path),
                    objectToApplyTo);
            }

            // are both properties of the same type?

            PropertyInfo fromProperty = PropertyHelpers.FindProperty(objectToApplyTo, operation.from);
            PropertyInfo pathProperty = PropertyHelpers.FindProperty(objectToApplyTo, operation.path);

            if (!(fromProperty.PropertyType == pathProperty.PropertyType))
            {
                throw new JsonPatchException<T>(operation,
                    string.Format("Path failed: property at path {0} has a different type than property at from {1}",
                    operation.path,
                    operation.from),
                    objectToApplyTo);
            }

            // if it exists, and it's of the same type as the "from" property, 
            // copy over the value of "from" to "path"

            PropertyHelpers.CopyValue(objectToApplyTo, fromProperty, pathProperty);
        }

    }
}
