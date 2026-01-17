using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    abstract class People
    {
        public People(string _name, char _gender)
        {
            name = _name;
            gender = _gender;
        }
        public string name { get; protected set; }
        public char gender { get; protected set; }

        public void changeName()
        {
            name = Console.ReadLine();
        }
        public void changeGender()
        {
            gender = Convert.ToChar(Console.ReadLine());
        }
        public virtual void getInfo()
        {
            string _gender = (gender == 'М') ? "мужского" : "женского";
            Console.WriteLine($"Я {name}. Я {_gender} пола.");
        }
    }
}