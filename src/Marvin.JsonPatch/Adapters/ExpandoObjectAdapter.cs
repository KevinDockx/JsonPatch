using Marvin.JsonPatch.Exceptions;
using Marvin.JsonPatch.Helpers;
using Marvin.JsonPatch.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Marvin.JsonPatch.Adapters
{
    //public class ExpandoObjectAdapter : IDynamicObjectAdapter<ExpandoObject>
    //{

    //    public void Add(Operations.Operation operation, ExpandoObject objectToApplyTo)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Copy(Operations.Operation operation, ExpandoObject objectToApplyTo)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Move(Operations.Operation operation, ExpandoObject objectToApplyTo)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Remove(Operations.Operation operation, ExpandoObject objectToApplyTo)
    //    {
    //        Remove(operation.path, objectToApplyTo, operation);
    //    }

    //    /// <summary>
    //    /// Remove is used by various operations (eg: remove, move, ...), yet through different operations;
    //    /// This method allows code reuse yet reporting the correct operation on error
    //    /// </summary>
    //    private void Remove(string path, ExpandoObject objectToApplyTo, Operation operationToReport)
    //    {

    //        bool removeFromList = false;
    //        int positionAsInteger = -1;
    //        string actualPathToProperty = path;

    //        if (path.EndsWith("/-"))
    //        {
    //            removeFromList = true;
    //            actualPathToProperty = path.Substring(0, path.Length - 2);
    //        }
    //        else
    //        {
    //            positionAsInteger = PropertyHelpers.GetNumericEnd(path);

    //            if (positionAsInteger > -1)
    //            {
    //                actualPathToProperty = path.Substring(0,
    //                    path.IndexOf('/' + positionAsInteger.ToString()));
    //            }
    //        }


    //        // does the target location exist?
    //        if (!(PropertyHelpers.CheckIfPropertyExists(objectToApplyTo, actualPathToProperty)))
    //        {
    //            throw new JsonPatchException(operationToReport,
    //                string.Format("Patch failed: property at location path: {0} does not exist", path),
    //                objectToApplyTo);
    //        }

    //        // get the property, and remove it - in this case, for DTO's, that means setting
    //        // it to null or its default value; in case of an array, remove at provided index
    //        // or at the end.

    //        PropertyInfo pathProperty = PropertyHelpers.FindProperty(objectToApplyTo, actualPathToProperty);


    //        if (removeFromList || positionAsInteger > -1)
    //        {

    //            var isNonStringArray = !(pathProperty.PropertyType == typeof(string))
    //                && typeof(IList).IsAssignableFrom(pathProperty.PropertyType);

    //            // what if it's an array but there's no position??
    //            if (isNonStringArray)
    //            {
    //                // now, get the generic type of the enumerable
    //                var genericTypeOfArray = PropertyHelpers.GetEnumerableType(pathProperty.PropertyType);

    //                // get value (it can be cast, we just checked that)
    //                var array = PropertyHelpers.GetValue(pathProperty, objectToApplyTo) as IList;

    //                if (removeFromList)
    //                {
    //                    array.RemoveAt(array.Count - 1);
    //                }
    //                else
    //                {
    //                    if (positionAsInteger < array.Count)
    //                    {
    //                        array.RemoveAt(positionAsInteger);
    //                    }
    //                    else
    //                    {
    //                        throw new JsonPatchException(operationToReport,
    //                   string.Format("Patch failed: provided path is invalid for array property type at location path: {0}: position larger than array size",
    //                   path),
    //                   objectToApplyTo);
    //                    }
    //                }

    //            }
    //            else
    //            {
    //                throw new JsonPatchException(operationToReport,
    //                   string.Format("Patch failed: provided path is invalid for array property type at location path: {0}: expected array",
    //                   path),
    //                   objectToApplyTo);
    //            }
    //        }
    //        else
    //        {

    //            // setting the value to "null" will use the default value in case of value types, and
    //            // null in case of reference types
    //            PropertyHelpers.SetValue(pathProperty, objectToApplyTo, null);
    //        }

    //    }

    //    public void Replace(Operations.Operation operation, ExpandoObject objectToApplyTo)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Test(Operations.Operation operation, ExpandoObject objectToApplyTo)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
