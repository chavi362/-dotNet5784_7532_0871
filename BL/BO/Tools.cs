using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace BO
{
    internal static class Tools
    {
        public static string ToStringProperty(this object obj)
        {
            if (obj == null)
                return "null";
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            string str = "";
            str+=($"{type.Name} properties:");

            foreach (PropertyInfo property in properties)
            {
         
               str+=($"{property.Name}: ");

                // Check if the property is a collection type (IEnumerable)
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) &&
                    property.PropertyType != typeof(string))
                {
                    // If it's a collection, iterate through its elements
                    IEnumerable collection = (IEnumerable)property.GetValue(obj)!;
                    if (collection != null)
                    {
                        foreach (var item in collection)
                        {
                            str+=($"{item}, ");
                        }
                    }
                }
                else
                {
                    // If it's not a collection, just get the property value
                    str+=($"{property.GetValue(obj)}, ");
                }
            }

            return str;
        }
    }
}
