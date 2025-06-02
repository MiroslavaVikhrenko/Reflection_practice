using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task3
{
    public class Date : IDate
    {
        private int day;
        private int month;
        private int year;

        public void SetDay(int day) => this.day = day;
        public void SetMonth(int month) => this.month = month;
        public void SetYear(int year) => this.year = year;

        public int GetDay() => day;
        public int GetMonth() => month;
        public int GetYear() => year;
    }
}
