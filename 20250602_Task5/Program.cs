namespace _20250602_Task5
{
    /*
     Используя рефлексию, создать собственный AutoMapper, для маппинга свойств двух классов:
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            var worker = new Worker("Alice", "Smith", 30, "Engineer");

            var client = AutoMapper.Map<Worker, Client>(worker);

            Console.WriteLine("Mapped Client object:");
            Console.WriteLine($"FirstName: {client.FirstName}");
            Console.WriteLine($"LastName : {client.LastName}");
            Console.WriteLine($"Age      : {client.Age}");
            Console.WriteLine($"Email    : {client.Email ?? "[null]"}");

            Console.ReadLine();
        }
    }
}
