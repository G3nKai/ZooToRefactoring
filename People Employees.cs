using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    class Employees : People
    {
        public List<Cage> responsibility { get; private set; } = new List<Cage>();
        public int occupation { get;  set; }//1 - убрщик, 2 - кипер изменить должность можно только в edit()
        public Employees(string name, char gender, int occupation) : base(name, gender) 
        {
            this.occupation = occupation;
        }

        public override void getInfo()
        {
            string _gender = (gender == 'М') ? "мужского" : "женского";
            Console.Write($"Я {name}. Я {_gender} пола. ");

            if (occupation == 1) Console.WriteLine("Я уборщик.");
            else Console.WriteLine("Я кипер.");
        }
        public void getAnimalToWork(List<Cage> cages)
        {
            if (occupation == 1)
            {
                Console.WriteLine("Я уборщик и не занимаюсь кормлением животных.");
                return;
            }

            bool _isEmpty = true;
            Cage resp = null;
            foreach (var cage in responsibility)
            {
                if (cage.animalsInCommon.Count > 0)
                {
                    _isEmpty = false;
                    resp = cage;
                    break;
                }
            }

            if (_isEmpty)
            {
                Console.WriteLine("Нет ни одного животного...\n" +
                    "Вы перебрасываетесь в меню добавления.");
                return;
            }

            Console.WriteLine("Список вольеров:");

            foreach (var cage in cages)
                ((ICage)cage).getInfo();

            Console.Write("Выберите вольер: ");
            int chosenCage = int.Parse(Console.ReadLine());


            if (resp != null)
                responsibility.Add(resp);

            Console.WriteLine();

            
        }
        public void getFeeded()//
        {
            if (occupation == 2)
                foreach (var cage in responsibility)
                {
                    if (((ICage)cage)._isFoodHere())
                        ((ICage)cage).foodFill();
                }
        }
        public void changeOccupation(int value)
        {
            occupation = value;
        }
    }
}
