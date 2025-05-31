using System.Reflection;

namespace Reflection_practice
{
    /*
     Создать класс, содержащий 3 любых метода. Получить информацию о методах данного класса.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            MyClass myclass = new MyClass() { Name= "bj"};

            Type mytype = Type.GetType("Reflection_practice.MyClass", false, true);

            foreach (MethodInfo method in mytype.GetMethods())
            {
                Console.WriteLine($"Method name: {method.Name},\n Return type: {method.ReturnType},\nIs public: {method.IsPublic},\n Is private {method.IsPrivate}");

                Console.WriteLine();
            }

            var privateField = typeof(MyClass).GetField("number", BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine($"Private field via reflection: {privateField.GetValue(myclass)}");
        }
    }

    public class MyClass
    {
        public string Name { get; set; }
        private int number;
        public MyClass() { number = 0; }
        public void Method1() { }
        private int Method2 (int n) { return 0; }
        public string Method3() { return " "; }
    }
}
