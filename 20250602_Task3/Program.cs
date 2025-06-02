using System.Reflection;

namespace _20250602_Task3
{
    /*
     Создать класс Date, который реализует интерфейс IDate, с полями: номер, месяц, год, 
    методами установки значений полям, методами получений значений. Вывести интерфейсы, который реализует класс и 
    его методы (методы объявленные в интерфейсе).
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Type dateType = typeof(Date);

            Console.WriteLine("Interfaces implemented by Date class:");
            foreach (Type iface in dateType.GetInterfaces())
            {
                Console.WriteLine($"- {iface.Name}");

                Console.WriteLine("  Methods in the interface:");
                foreach (MethodInfo method in iface.GetMethods())
                {
                    Console.WriteLine($"    {method.ReturnType.Name} {method.Name}()");
                }
            }
        }
    }
}
