using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMonMD
{
    public class Doctor
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string HomeTown { get; set; }
        public string ID { get; set; }
        public string Sex { get; set; }

        public Doctor (string name, string age, string hometown, string id, string sex){
            Name = name;
            Age = age;
            HomeTown = hometown;
            ID = id;
            Sex = sex;
        }
    }
}
