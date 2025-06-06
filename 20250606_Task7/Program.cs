using System.Reflection;

namespace _20250606_Task7
{
    /*
     С помощью рефлексии можно получать дополнительные сведения о типе. 
    Типом может быть класс, структура, интерфейс, перечисление, делегат. 
    Выбрать любой тип и получить о нем полную информацию
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Type type = typeof(string); 

            Console.WriteLine($"Type: {type.FullName}");
            Console.WriteLine($"Namespace: {type.Namespace}");
            Console.WriteLine($"Assembly: {type.Assembly.FullName}");
            Console.WriteLine($"Base Type: {type.BaseType}");
            Console.WriteLine($"Is Class: {type.IsClass}");
            Console.WriteLine($"Is Struct: {type.IsValueType && !type.IsPrimitive}");
            Console.WriteLine($"Is Interface: {type.IsInterface}");
            Console.WriteLine($"Is Enum: {type.IsEnum}");
            Console.WriteLine($"Is Delegate: {typeof(MulticastDelegate).IsAssignableFrom(type.BaseType)}");

            Console.WriteLine("\n--- Constructors ---");
            foreach (var ctor in type.GetConstructors())
            {
                Console.WriteLine(ctor);
            }

            Console.WriteLine("\n--- Properties ---");
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine($"{prop.PropertyType.Name} {prop.Name}");
            }

            //Console.WriteLine("\n--- Methods ---");
            //foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            //{
            //    Console.WriteLine($"{method.ReturnType.Name} {method.Name}()");
            //}
            Console.WriteLine("\n--- Methods ---");
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                if (method.IsSpecialName) continue; // Skip property accessors and operators
                Console.WriteLine($"{method.ReturnType.Name} {method.Name}({string.Join(", ", method.GetParameters().Select(p => p.ParameterType.Name))})");
            }

            Console.WriteLine("\n--- Unique Method Names ---");
            foreach (var name in methods.Where(m => !m.IsSpecialName).Select(m => m.Name).Distinct())
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("\n--- Fields ---");
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                Console.WriteLine($"{field.FieldType.Name} {field.Name}");
            }

            Console.WriteLine("\n--- Interfaces ---");
            foreach (var iface in type.GetInterfaces())
            {
                Console.WriteLine(iface.FullName);
            }

            Console.WriteLine("\n--- Attributes ---");
            foreach (var attr in type.GetCustomAttributes(false))
            {
                Console.WriteLine(attr.GetType().Name);
            }

            Console.ReadLine(); 
        }
    }
}
