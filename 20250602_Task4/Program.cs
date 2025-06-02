using System.Reflection;

namespace _20250602_Task4
{
    /*
     Используя рефлексию, создайте универсальный метод, позволяющий выводить данные из коллекции типа <T>, 
    в виде таблицы. Так же, реализуйте, возможность передачи названий свойств коллекции, которые необходимо исключить 
    при построении таблицы.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>
            {
                new Person { FirstName = "Alice", LastName = "Smith", Age = 30, Email = "alice@example.com" },
                new Person { FirstName = "Bob", LastName = "Johnson", Age = 45, Email = "bob@example.com" },
                new Person { FirstName = "Charlie", LastName = "Williams", Age = 28, Email = "charlie@example.com" }
            };

            Console.WriteLine("=== Full Table ===");
            PrintTable(people);

            Console.WriteLine("\n=== Exclude 'Email' and 'Age' ===");
            PrintTable(people, "Email", "Age");

            Console.ReadLine();
        }

        public static void PrintTable<T>(IEnumerable<T> items, params string[] excludedProperties)
        {
            if (items == null || !items.Any())
            {
                Console.WriteLine("No data to display.");
                return;
            }

            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => !excludedProperties.Contains(p.Name))
                                 .ToArray();

            foreach (var prop in properties)
            {
                Console.Write($"{prop.Name,-20}");
            }
            Console.WriteLine();

            Console.WriteLine(new string('-', properties.Length * 20));

            foreach (var item in items)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item);
                    Console.Write($"{value,-20}");
                }
                Console.WriteLine();
            }
        }
    }


}
