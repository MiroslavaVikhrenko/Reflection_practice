namespace _20250602_Task6
{
    /*
     Создайте универсальный сериализатор объектов в тип данных Json, который может работать с различными классами, 
    не зная их типа заранее. Вам необходимо использовать рефлексию для анализа свойств объекта и сохранения / 
    восстановления его состояния. Для строк и дат используйте двойные кавычки, для чисел – не используйте. 
    Стерилизованные данные должны выглядеть подобным образом:

{
"FirstName": "John",
"LastName": "Doe",
"Age": 30,
"Position": "Developer"
}

Создайте интерфейс ISerializable, который будет служить основной отправной точкой для вашего класса сериализатора
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            var worker = new Worker("John", "Doe", 30, "Developer");

            ISerializable serializer = new JsonReflectiveSerializer();

            string json = serializer.Serialize(worker);
            Console.WriteLine("Serialized JSON:");
            Console.WriteLine(json);

            var deserializedWorker = serializer.Deserialize<Worker>(json);
            Console.WriteLine("\nDeserialized Object:");
            Console.WriteLine($"{deserializedWorker.FirstName} {deserializedWorker.LastName}, Age {deserializedWorker.Age}, Position: {deserializedWorker.Position}");

            Console.ReadLine();
        }
    }
}
