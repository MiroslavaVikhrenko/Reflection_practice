using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task2
{
    public class Person
    {
        private int age;
        public int Age
        {
            get => age;
            set => age = value;
        }
        public Person(int age)
        {
            this.age = age;
        }
    }
}
