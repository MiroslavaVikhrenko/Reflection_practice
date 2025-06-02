using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task5
{
    public class Worker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public Worker(string firstName, string lastName, int age, string position)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Position = position;
        }
    }
}
