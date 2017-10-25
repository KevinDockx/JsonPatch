// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using Marvin.JsonPatch.Exceptions;
using Marvin.JsonPatch.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marvin.JsonPatch.Helpers
{
    internal static class PropertyHelpers
    {
        public static object GetValue(PropertyInfo propertyToGet, object targetObject, string pathToProperty)
        {
            // it is possible the path refers to a nested property.  In that case, we need to 
            // get from a different target object: the nested object. 

            // split the propertypath, and if necessary, remove the first 
            // empty item (that's the case when it starts with a "/")
            var splitPath = pathToProperty.Split(
                new char[] { '/' },
                StringSplitOptions.RemoveEmptyEntries).ToList(); 
             
            for (int i = 0; i < splitPath.Count - 1; i++)
            {
                // if the current part of the path is numeric, this means we're trying
                // to get the propertyInfo of a specific object in an array.  To allow
                // for this, the previous value (targetObject) must be an IEnumerable, and
                // the position must exist.

                int numericValue = -1;
                if (int.TryParse(splitPath[i], out numericValue))
                {
                    var element = GetElementAtFromObject(targetObject, numericValue);
                    if (element != null)
                    {
                        targetObject = element;
                    }
                    else
                    {
                        // will result in JsonPatchException in calling class, as expected
                        return null;
                    }
                }
                else
                {
                    var propertyInfoToGet = GetPropertyInfo(targetObject, splitPath[i]
                    , BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    targetObject = propertyInfoToGet.GetValue(targetObject, null);
                }
            }
            return propertyToGet.GetValue(targetObject, null);
        }

        public static bool SetValue(PropertyInfo propertyToSet, object targetObject, string pathToProperty, object value)
        {
            // it is possible the path refers to a nested property.  In that case, we need to 
            // set on a different target object: the nested object.
         
            // split the propertypath, and if necessary, remove the first 
            // empty item (that's the case when it starts with a "/")
            var splitPath = pathToProperty.Split(
                new char[] { '/' },
                StringSplitOptions.RemoveEmptyEntries).ToList();  

            for (int i = 0; i < splitPath.Count - 1; i++)
            {
                // if the current part of the path is numeric, this means we're trying
                // to get the propertyInfo of a specific object in an array.  To allow
                // for this, the previous value (targetObject) must be an IEnumerable, and
                // the position must exist.

                int numericValue = -1;
                if (int.TryParse(splitPath[i], out numericValue))
                {
                    var element = GetElementAtFromObject(targetObject, numericValue);
                    if (element != null)
                    {
                        targetObject = element;
                    }
                    else
                    {
                        // will result in JsonPatchException in calling class, as expected
                        return false;
                    }
                }
                else
                {
                    var propertyInfoToGet = GetPropertyInfo(targetObject, splitPath[i]
                    , BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    targetObject = propertyInfoToGet.GetValue(targetObject, null);
                }
            }

            propertyToSet.SetValue(targetObject, value, null);
            return true;
        }
 

        public static PropertyInfo FindProperty(object targetObject, string pathToProperty)
        {
            try
            {
                // split the propertypath, and if necessary, remove the first 
                // empty item (that's the case when it starts with a "/")
                var splitPath = pathToProperty.Split(
                    new char[] { '/' },
                    StringSplitOptions.RemoveEmptyEntries).ToList(); 

                for (int i = 0; i < splitPath.Count - 1; i++)
                {
                    // if the current part of the path is numeric, this means we're trying
                    // to get the propertyInfo of a specific object in an array.  To allow
                    // for this, the previous value (targetObject) must be an IEnumerable, and
                    // the position must exist.

                    int numericValue = -1;
                    if (int.TryParse(splitPath[i], out numericValue))
                    {
                        var element = GetElementAtFromObject(targetObject, numericValue);
                        if (element != null)
                        {
                            targetObject = element;
                        }
                        else
                        {
                            // will result in JsonPatchException in calling class, as expected
                            return null;
                        }
                    }
                    else
                    {
                        var propertyInfoToGet = GetPropertyInfo(targetObject, splitPath[i]
                        , BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        targetObject = propertyInfoToGet.GetValue(targetObject, null);
                    }
                }

                var matches = targetObject
                                .GetType()
                                .GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                                .Where(p => p.Name.ToLower().Equals(splitPath.Last().ToLower()))
                                .ToArray(); //If type is derived, this query will get all properties of base types and declaring type.

                return matches.FirstOrDefault(p => p.DeclaringType == targetObject.GetType()) ?? matches.FirstOrDefault(); //If multiple properties exist with same name, preferentially returns PropertyInfo of derived type 
            }
            catch (Exception)
            {
                // will result in JsonPatchException in calling class, as expected
                return null;
            }
        }

        private static object GetElementAtFromObject(object targetObject, int numericValue)
        {
            if (numericValue > -1)
            {
                // Check if the targetobject is an IEnumerable,
                // and if the position is valid.
                if (targetObject is IEnumerable)
                {
                    var indexable = ((IEnumerable)targetObject).Cast<object>();

                    if (indexable.Count() >= numericValue)
                    {
                        return indexable.ElementAt(numericValue);
                    }
                    else { return null; }
                }
                else { return null; ; }
            }
            else { return null; }
        }

        internal static ConversionResult ConvertToActualType(Type propertyType, object value)
        {
            try
            {
                var o = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), propertyType);
                return new ConversionResult(true, o);
            }
            catch (Exception)
            {
                return new ConversionResult(false, null);
            }
        }
        
        internal static Type GetEnumerableType(Type type)
        {
            if (type == null) throw new ArgumentNullException();
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return interfaceType.GetGenericArguments()[0];
                }
            }
            return null;
        }
        
        private static PropertyInfo GetPropertyInfo(object targetObject, string propertyName,
        BindingFlags bindingFlags)
        {
            return targetObject.GetType().GetProperty(propertyName, bindingFlags);
        }

        internal static ActualPropertyPathResult GetActualPropertyPath(
            string propertyPath,
            object objectToApplyTo,
            OperationBase operationToReport,
            bool forPath)
        {
            if (propertyPath.EndsWith("/-"))
            {
                return new ActualPropertyPathResult(-1, propertyPath.Substring(0, propertyPath.Length - 2), true);
            }
            else
            {                
                var possibleIndex = propertyPath.Substring(propertyPath.LastIndexOf("/") + 1);
                int castedIndex = -1;
                if (int.TryParse(possibleIndex, out castedIndex))
                {
                    // has numeric end.  
                    if (castedIndex > -1)
                    {
                        var pathToProperty = propertyPath.Substring(
                           0,
                           propertyPath.LastIndexOf('/' + castedIndex.ToString()));

                        return new ActualPropertyPathResult(castedIndex, pathToProperty, false);
                    }
                    else
                    {
                        string message = forPath ?
                             string.Format("Patch failed: provided path is invalid, position too small: {0}", propertyPath)
                             : string.Format("Patch failed: provided from is invalid, position too small: {0}", propertyPath);

                        // negative position - invalid path
                        throw new JsonPatchException(
                             new JsonPatchError(objectToApplyTo,
                                 operationToReport,
                              message), 422);
                    }
                }
                return new ActualPropertyPathResult(-1, propertyPath, false);
            }
        }

        internal static bool IsNonStringList(this Type propertyType)
        {
            var isNonString = propertyType != typeof(string);
            var isList = typeof(IList).IsAssignableFrom(propertyType);
            var isGenericList = propertyType.ImplementsGeneric(typeof(IList<>));
            return isNonString && (isList || isGenericList);
        }

        internal static bool ImplementsGeneric(this Type propertyType, Type genericInterfaceDefinition)
        {
            if (genericInterfaceDefinition == null)
                throw new ArgumentNullException();

            if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
                throw new ArgumentNullException();

            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == genericInterfaceDefinition)
                return true;

            return propertyType.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Select(i => i.GetGenericTypeDefinition())
                .Contains(genericInterfaceDefinition);
        }

        //internal static bool IsNonStringArray(Type type)
        //{
        //    if (GetIListType(type) != null)
        //    {
        //        return true;
        //    }

        //    return (!(type == typeof(string)) && typeof(IList)
        //        .GetType().IsAssignableFrom(type.GetType()));
        //}

        //internal static bool IsGenericListType(Type type)
        //{
        //    if (type == null)
        //        throw new ArgumentException("Parameter type cannot be null");

        //    if (type.GetType().IsGenericType &&
        //            type.GetGenericTypeDefinition() == typeof(IList<>))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //internal static Type GetIListType(Type type)
        //{
        //    if (type == null)
        //        throw new ArgumentException("Parameter type cannot be null");

        //    if (IsGenericListType(type))
        //    {
        //        return type.GetGenericArguments()[0];
        //    }

        //    foreach (Type interfaceType in type.GetType().GetInterfaces())
        //    {
        //        if (IsGenericListType(interfaceType))
        //        {
        //            return interfaceType.GetGenericArguments()[0];
        //        }
        //    }
        //    return null;
        //}
    }
}