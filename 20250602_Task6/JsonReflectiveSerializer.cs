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
        // Serializes an object of any type T into a JSON-formatted string
        public string Serialize<T>(T obj)
        {
            // If the object is null, return an empty JSON object
            if (obj == null) return "{}";

            // Use reflection to get all public instance properties of the object
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // StringBuilder for efficient string concatenation
            var sb = new StringBuilder();
            sb.AppendLine("{"); // Start JSON object

            // Iterate through each property
            for (int i = 0; i < properties.Length; i++)
            {
                var prop = properties[i]; // Get property info
                var name = prop.Name; // Get property name
                var value = prop.GetValue(obj); // Get property value from object
                var serializedValue = FormatValue(value); // Format value to JSON-compatible format

                // Append property name and value to the string
                sb.Append($"\"{name}\": {serializedValue}");

                // Add a comma unless this is the last property
                if (i < properties.Length - 1)
                    sb.Append(",");

                sb.AppendLine(); // Move to next line
            }

            sb.Append("}"); // End JSON object
            return sb.ToString(); // Return the built JSON string
        }

        // Deserializes a JSON-formatted string into an object of type T
        public T Deserialize<T>(string data)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("Input JSON is null or empty.");

            // Create an instance of the type T
            T obj = Activator.CreateInstance<T>();

            // Remove braces and whitespace, then split lines
            var lines = data.Trim('{', '}', '\r', '\n')
                            .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Store key-value pairs from JSON
            var dict = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                // Split the line into key and value based on the first colon
                var parts = line.Trim().Trim(',').Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    // Clean up key and value strings
                    var key = parts[0].Trim().Trim('"');
                    var value = parts[1].Trim().Trim('"');
                    dict[key] = value; // Add to dictionary
                }
            }
            // Get public instance properties of type T
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Set values on the object from dictionary if property exists
            foreach (var prop in properties)
            {
                if (dict.TryGetValue(prop.Name, out string stringValue))
                {
                    // Convert string to actual property type
                    object convertedValue = ConvertStringToType(stringValue, prop.PropertyType);
                    // Set the value on the object
                    prop.SetValue(obj, convertedValue);
                }
            }

            return obj; // Return the populated object
        }

        // Helper method to format a value for JSON output
        private string FormatValue(object value)
        {
            if (value == null) return "null";

            // Wrap strings and dates in quotes
            if (value is string || value is DateTime)
                return $"\"{value}\"";

            // Convert boolean to lowercase
            if (value is bool)
                return value.ToString().ToLower();

            // Use invariant culture for numbers
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        // Converts a string value into a specified target type
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

            // Return null for unsupported types
            return null;
        }
    }
}
