using System.Reflection;
using System.Text.Json.Serialization;

namespace _20250606_Task8
{
    /*
     У вас есть несколько DTO (Data Transfer Object) классов, которые используются для сериализации данных в API. 
    Необходимо с помощью рефлексии написать инспектор, который проверяет, соответствуют ли эти классы следующим условиям:

1) Все свойства должны быть публичными и иметь геттер и сеттер. 
2) Все свойства должны быть простыми типами (int, string, DateTime, bool, и т.д. — список задается). 
3) Класс не должен содержать методы. 
4) Все свойства должны быть помечены атрибутом [JsonPropertyName].
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            // Add DTO types  to inspect
            var dtoTypes = new List<Type>
            {
                typeof(PersonDto) 
            };

            foreach (var type in dtoTypes)
            {
                Console.WriteLine($"Inspecting DTO: {type.Name}");
                InspectDtoType(type);
                Console.WriteLine(new string('-', 40));
            }

            Console.ReadLine(); 
        }

        static void InspectDtoType(Type type)
        {
            // Define allowed primitive types
            Type[] allowedTypes = new[]
            {
                typeof(int),
                typeof(string),
                typeof(DateTime),
                typeof(bool),
                typeof(double),
                typeof(float),
                typeof(decimal)
            };

            bool allValid = true;

            // Check that all properties are public with both getter and setter
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                if (!prop.CanRead || !prop.CanWrite)
                {
                    Console.WriteLine($"Property '{prop.Name}' must have both getter and setter.");
                    allValid = false;
                }

                // Check that property is a simple/allowed type
                if (!allowedTypes.Contains(prop.PropertyType))
                {
                    Console.WriteLine($"Property '{prop.Name}' must be a simple type, found: {prop.PropertyType.Name}.");
                    allValid = false;
                }

                // Check for [JsonPropertyName] attribute
                bool hasJsonAttr = prop.GetCustomAttribute<JsonPropertyNameAttribute>() != null;
                if (!hasJsonAttr)
                {
                    Console.WriteLine($"Property '{prop.Name}' must have [JsonPropertyName] attribute.");
                    allValid = false;
                }
            }

            // Check that class has no public methods other than property accessors
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                if (!method.IsSpecialName) // Skip property getters/setters
                {
                    Console.WriteLine($"DTO class '{type.Name}' should not contain method '{method.Name}'.");
                    allValid = false;
                }
            }

            if (allValid)
            {
                Console.WriteLine("All checks passed.");
            }
        }
    }
}
