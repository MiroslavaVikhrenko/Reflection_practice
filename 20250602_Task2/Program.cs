namespace _20250602_Task2
{
    internal class Program
    {
        /*
        
        Вам необходимо добавить возможность отслеживать изменения возраста объекта Person с использованием рефлексии. 
        Создайте новый класс, например, AgeTracker, который будет отслеживать изменения поля age и выводить сообщение 
        при каждом изменении. После каждого изменения поля age, вызывайте метод класса AgeTrackerдля сравнения значения.
         */
        static void Main(string[] args)
        {
            Person person = new Person(25);
            AgeTracker tracker = new AgeTracker(person);

            person.Age = 30;
            tracker.TrackAge(person); 

            person.Age = 35;
            tracker.TrackAge(person); 

            person.Age = 35;
            tracker.TrackAge(person); 
        }
    }
}
