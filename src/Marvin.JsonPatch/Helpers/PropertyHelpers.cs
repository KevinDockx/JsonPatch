// Kevin Dockx
//
// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/JsonPatch
//
// Enjoy :-)

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Marvin.JsonPatch.Helpers
{
    internal static class PropertyHelpers
    {
        public static bool FindAndSetProperty(object targetObject, string propertyPath, object value)
        {
            try 
	        {	        

                    string[] bits = propertyPath.Split('/');

                    // skip the first one if it's empty

                    int startIndex = (string.IsNullOrWhiteSpace(bits[0])  ? 1 : 0);


                    for (int i = startIndex; i < bits.Length - 1; i++)
                    {
                        PropertyInfo propertyToGet = targetObject.GetType().GetProperty(bits[i]);
                        targetObject = propertyToGet.GetValue(targetObject, null);
                    }

                    PropertyInfo propertyToSet = targetObject.GetType().GetProperty(bits.Last(), 
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    propertyToSet.SetValue(targetObject, value, null);

                        return true;
                		
	        }
	        catch (Exception)
	        {
		        return false;
	        }
        }

        public static bool SetValue(PropertyInfo propertyToSet, object targetObject, object value)
        {  
            propertyToSet.SetValue(targetObject, value, null);
            return true;
        }

        public static object GetValue(PropertyInfo propertyToGet, object targetObject)
        {
            return propertyToGet.GetValue(targetObject, null);
        }


        public static bool CheckIfPropertyExists(object targetObject, string propertyPath)
        {
            try
            {

                string[] splitPath = propertyPath.Split('/');

                // skip the first one if it's empty
                int startIndex = (string.IsNullOrWhiteSpace(splitPath[0]) ? 1 : 0);

                for (int i = startIndex; i < splitPath.Length - 1; i++)
                {
                    PropertyInfo propertyInfoToGet = targetObject.GetType().GetProperty(splitPath[i]);
                    targetObject = propertyInfoToGet.GetValue(targetObject, null);
                }

                // for dynamic objects
                if (targetObject is IDynamicMetaObjectProvider)
                {
                    IDynamicMetaObjectProvider target = targetObject as IDynamicMetaObjectProvider;
                    var propList = target.GetMetaObject(Expression.Constant(target)).GetDynamicMemberNames();
                    return propList.Contains(splitPath.Last()); 
                }
                else
                {
                    PropertyInfo propertyToCheck = targetObject.GetType().GetProperty(splitPath.Last(),
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    return propertyToCheck != null;
                }
          
            }
            catch (Exception)
            {
                return false;
            }
        }



        public static PropertyInfo FindProperty(object targetObject, string propertyPath)
        {
            try
            {

                string[] splitPath = propertyPath.Split('/');

                // skip the first one if it's empty
                int startIndex = (string.IsNullOrWhiteSpace(splitPath[0]) ? 1 : 0);

                for (int i = startIndex; i < splitPath.Length - 1; i++)
                {
                    PropertyInfo propertyToGet = targetObject.GetType().GetProperty(splitPath[i]);
                    targetObject = propertyToGet.GetValue(targetObject, null);
                }

                PropertyInfo propertyToFind = targetObject.GetType().GetProperty(splitPath.Last(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                return propertyToFind;
            }
            catch (Exception)
            {
                return null;
            }
        }


        internal static bool CheckIfValueCanBeCast(Type propertyType, object value)
        {
            try
            {
                   Convert.ChangeType(value, propertyType);
                   return true;
            }
            catch (Exception)
            {
                return false;
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



        internal static int GetNumericEnd(string path)
        {
            string possibleIndex = path.Substring(path.LastIndexOf("/") + 1);
            int castedIndex = -1;

            if (int.TryParse(possibleIndex, out castedIndex))
            {
                return castedIndex;
            }

            return -1;

        }

 
    }
}
