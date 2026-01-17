using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    class Visitors : People
    {
        Random rnd = new Random();
        private int money;
        public Visitors(string name, char gender) : base(name, gender) 
        {
            this.name = name;
            this.gender = gender;
            int money = rnd.Next(150, 671);
        }
        public void feedAnimal(Cage cage, Animal animal) 
        {
            if (cage.animalsOpened.Contains(animal) && animal.isHungry) 
            {
                int food = buyFood();
                animal.gettingFeeded(food);
            }
        }
        public int buyFood()
        {
            int cracker;
            //1 единица валюты - 6 сытости
            if (money - 1 > 0)
            {
                money--;
                cracker = 6;
                return cracker;
            }
            cracker = 0;
            return cracker;
        }
    }
}
