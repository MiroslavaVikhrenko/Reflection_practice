using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task5
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Client() { }

        public Client(string firstName, string lastName, int age, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Email = email;
        }
    }
}
