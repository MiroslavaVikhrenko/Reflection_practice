using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task3
{
    public interface IDate
    {
        void SetDay(int day);
        void SetMonth(int month);
        void SetYear(int year);

        int GetDay();
        int GetMonth();
        int GetYear();
    }
}
