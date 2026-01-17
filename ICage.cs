using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    interface ICage
    {
        int countAnimals();//вызовет две функции
        void addAnimal(Animal animal);
        void removeAnimal(int index);
        EnumAnimalName checkSpecies();
        void checkFood();
        bool checkThreshold();//количество допустимых животных в зависимости от вида
        void setThreshold();
        bool _isFoodHere();
        void changeFood(int amount);
        void foodFill();
        void getInfo();
        void getSlightInfoForDirector();
    }
}
