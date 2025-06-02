using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task2
{
    public class AgeTracker
    {
        private int previousAge;

        public AgeTracker(Person person)
        {
            previousAge = GetPrivateAge(person);
        }

        public void TrackAge(Person person)
        {
            int currentAge = GetPrivateAge(person);
            if (currentAge != previousAge)
            {
                Console.WriteLine($"Age changed from {previousAge} to {currentAge}");
                previousAge = currentAge;
            }
        }

        private int GetPrivateAge(Person person)
        {
            // Use reflection to access private field
            FieldInfo ageField = typeof(Person).GetField("age", BindingFlags.NonPublic | BindingFlags.Instance);
            return (int)ageField.GetValue(person);
        }
    }
}
