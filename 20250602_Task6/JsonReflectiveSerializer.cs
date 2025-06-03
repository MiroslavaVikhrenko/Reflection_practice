using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task6
{
    public class JsonReflectiveSerializer : ISerializable
    {
        public string Serialize<T>(T obj)
        {
            if (obj == null) return "{}";

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var sb = new StringBuilder();
            sb.AppendLine("{");

            for (int i = 0; i < properties.Length; i++)
            {
                var prop = properties[i];
                var name = prop.Name;
                var value = prop.GetValue(obj);
                var serializedValue = FormatValue(value);

                sb.Append($"\"{name}\": {serializedValue}");

                if (i < properties.Length - 1)
                    sb.Append(",");

                sb.AppendLine();
            }

            sb.Append("}");
            return sb.ToString();
        }

        public T Deserialize<T>(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("Input JSON is null or empty.");

            T obj = Activator.CreateInstance<T>();

            var lines = data.Trim('{', '}', '\r', '\n')
                            .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var dict = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                var parts = line.Trim().Trim(',').Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim().Trim('"');
                    var value = parts[1].Trim().Trim('"');
                    dict[key] = value;
                }
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                if (dict.TryGetValue(prop.Name, out string stringValue))
                {
                    object convertedValue = ConvertStringToType(stringValue, prop.PropertyType);
                    prop.SetValue(obj, convertedValue);
                }
            }

            return obj;
        }

        private string FormatValue(object value)
        {
            if (value == null) return "null";

            if (value is string || value is DateTime)
                return $"\"{value}\"";

            if (value is bool)
                return value.ToString().ToLower();

            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private object ConvertStringToType(string value, Type targetType)
        {
            if (targetType == typeof(string))
                return value;

            if (targetType == typeof(int))
                return int.Parse(value, CultureInfo.InvariantCulture);

            if (targetType == typeof(double))
                return double.Parse(value, CultureInfo.InvariantCulture);

            if (targetType == typeof(bool))
                return bool.Parse(value);

            if (targetType == typeof(DateTime))
                return DateTime.Parse(value, CultureInfo.InvariantCulture);

            return null;
        }
    }
}
