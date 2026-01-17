using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zoo
{
    class Cage: ICage, ICageClosed, ICageOpened//будет создан список вольеров
    {
        private static byte threshold;
        private static int foodSupply;
        public List<Animal> animalsOpened = new List<Animal>();
        public List<Animal> animalsClosed = new List<Animal>();
        public List<Animal> animalsInCommon { get; private set; } = new List<Animal>();
        public Cage()
        {
            ((ICage)this).setThreshold();
            foodSupply = 0;
        }
        void ICage.changeFood(int amount)
        {
            if (foodSupply - amount < 0) foodSupply = 0;
            else foodSupply -= amount;
        }
        Type ICage.checkSpecies()
        {
            if (threshold == 5)
                return typeof(Wolf);
            else if (threshold == 8)
                return typeof(Monkey);
            else if (threshold == 13)
                return typeof(Parrot);
            return typeof(Animal);
        }
        bool ICage.checkThreshold()
        {
            return threshold > ((ICage)this).countAnimals();
        }
        bool ICage._isFoodHere()
        {
            if (foodSupply > 0)
                return true;

            return false;
        }
        void ICage.foodFill()
        {
            foodSupply += 210;
        }
        void ICage.checkFood()
        {
            Console.WriteLine($"Еда в вольере: {foodSupply}");
        }
        void ICage.getSlightInfoForDirector()
        {
            Console.WriteLine($"В закрытом вольере находится {((ICageClosed)this).countAnimalsClosed()} зверей.\n" +
                $"В открытом вольере находится {((ICageOpened)this).countAnimalsOpened()} зверей.");

            ((ICage)this).checkFood();

            if (animalsInCommon.Count > 0)
            {
                Console.WriteLine("Общий список животных, пронумерованный:\n");

                for (int i = 0; i < animalsInCommon.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    animalsInCommon[i].getInfo();
                }
            }

            Console.WriteLine();
        }
        void ICage.getInfo()
        {
            Console.WriteLine("Общий список животных, пронумерованный:\n");

            for (int i = 0; i < animalsInCommon.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                animalsInCommon[i].getInfo();
            }

            Console.WriteLine();
        }
        void ICageClosed.getInfo()
        {
            Console.WriteLine("В закрытой части вольера сейчас находятся:\n");

            for (int i = 0; i < animalsClosed.Count; i++)
            {
                Console.Write($"{i+1}. ");
                animalsClosed[i].getInfo();
            }
            Console.WriteLine();
        }
        void ICageOpened.getInfo()
        {
            Console.WriteLine("В открытой части вольера сейчас находятся:\n");

            for (int i = 0; i < animalsOpened.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                animalsOpened[i].getInfo();
            }
            Console.WriteLine();
        }
        
        void ICage.setThreshold()
        {
            if (animalsInCommon.Count == 0)
                threshold = 20;
            else if (animalsInCommon[0] is Wolf) 
                threshold = 5;
            else if (animalsInCommon[0] is Monkey)
                threshold = 8;
            else if (animalsInCommon[0] is Parrot)
                threshold = 13;
        }
        void ICage.addAnimal(Animal animal)
        {
            if (animalsInCommon.Count == 0 || animalsInCommon[0].GetType() == animal.GetType())
            {
                animalsOpened.Add(animal);
                animalsInCommon.Add(animal); 
                Console.WriteLine("Животное успешно добавлено в вольер.\n");
                return;
            }
            Console.WriteLine("В вольер нельзя садить разные виды животных...\n");
        }
        void ICage.removeAnimal(int index)
        {
            if (animalsOpened.Contains(animalsInCommon[index]))
            {
                animalsOpened?.Remove(animalsInCommon[index]);
                animalsInCommon?.Remove(animalsInCommon[index]);
            }
            else if (animalsClosed.Contains(animalsInCommon[index]))
            {
                animalsClosed?.Remove(animalsInCommon[index]);
                animalsInCommon?.Remove(animalsInCommon[index]);
            }

        }
        int ICage.countAnimals() 
        {
            int amount = 0;
            amount += ((ICageClosed)this).countAnimalsClosed();
            amount += ((ICageOpened)this).countAnimalsOpened();

            return amount;
        }
        int ICageClosed.countAnimalsClosed()
        {
            return animalsClosed.Count;
        }
        int ICageOpened.countAnimalsOpened()
        {
            return animalsOpened.Count;
        }
        void ICageOpened.moveToClosed(Animal animal)
        {
            animalsOpened.Remove(animal);
            animalsClosed.Add(animal);
        }
        void ICageClosed.moveToOpened(Animal animal)
        {
            animalsClosed.Remove(animal);
            animalsOpened.Add(animal);
        }
    }
}
