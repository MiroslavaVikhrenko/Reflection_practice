using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task6
{
    public interface ISerializable
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string data);
    }
}
