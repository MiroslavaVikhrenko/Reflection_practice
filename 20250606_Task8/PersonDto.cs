using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _20250606_Task8
{
    public class PersonDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        // To test method violation
        //public void TestMethod() { }

        // To test invalid type
        //public object ExtraData { get; set; }

        // To test missing attribute
        //public string Email { get; set; }
    }
}
