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
        private int oneDollar = 1;
        private int oneFood = 6;
        Random rnd = new Random();
        private int money;
        public Visitors(string name, char gender) : base(name, gender) 
        {
            this.name = name;
            this.gender = gender;
            int money = rnd.Next(150, 671);
        }
        public int buyFood(Cage cage, Animal animal) //
        {
            if (cage.animalsOpened.Contains(animal) && animal.isHungry) 
            {
                if (money - oneDollar > 0)
                {
                    money--;
                    return oneFood;
                }
            }
            return 0;
        }
    }
}
