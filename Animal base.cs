using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    class Animal
    {
        static int based = 100;
        public int state { protected get; set; } = based;
        public bool isHungry { get; set; } = false;
        public int hungerThreshold { get; protected set; }
        public EnumAnimalName animalName { get; set; }
        public void getInfo()
        {
            _isHungry();
            string hunger = (isHungry) ? "голоден" : "сыт";
            string species;

            switch (hungerThreshold)
            {
                case 80:
                    species = "Попугай";
                    break;
                case 60:
                    species = "Обезьяна";
                    break;
                default:
                    species = "Волк";
                    break;
            }

            Console.WriteLine($"Я {species}. Я {hunger}. Моя сытость сейчас равна {state}.");
        }
        public void _isHungry()
        {
            if (state <= hungerThreshold) isHungry = true; 
        }
        public void gettingHungry(List<Cage> cages, int index)
        {
            if (state > 0)
            {
                state -= 4;
                ((ICage)cages[index]).changeFood(4);
                
            }
            else state = 0;
        }
        public void gettingFeeded(int value)
        {
            state += value;
        }
        public void voice() 
        {
            if (animalName ==  EnumAnimalName.Monkey) Console.WriteLine("Обезьяна говорит: *бу-га-га*!\n");
            else if (animalName == EnumAnimalName.Parrot) Console.WriteLine("Попугай говорит: Плохой директор! Плохой!!!\n");
            else Console.WriteLine("Волк говорит: *Ву-у-у-у*!\n");
        }
        public void changeState(int value)
        {
            state = value;
        }

    }
}
