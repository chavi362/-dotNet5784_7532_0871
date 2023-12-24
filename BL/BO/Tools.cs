using System;
using System.Reflection;
using System.Text;

namespace BO
{
    internal static class Tools
    {
        public static string ToStringProperty<T>(this T obj)
        {
            if (obj == null)
                return "null";

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.Append($"{type.Name} properties:");

            foreach (PropertyInfo property in properties)
            {
                sb.AppendLine();
                sb.Append($"{property.Name}: ");

                // Check if the property is a collection type (IEnumerable)
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) &&
                    property.PropertyType != typeof(string))
                {
                    // If it's a collection, iterate through its elements
                    IEnumerable collection = (IEnumerable)property.GetValue(obj);
                    if (collection != null)
                    {
                        foreach (var item in collection)
                        {
                            sb.Append($"{item}, ");
                        }
                    }
                }
                else
                {
                    // If it's not a collection, just get the property value
                    sb.Append($"{property.GetValue(obj)}, ");
                }
            }

            return sb.ToString().TrimEnd(' ', ',');
        }
    }
}
