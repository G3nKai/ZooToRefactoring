using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    class Zoo
    {
        public List<Cage> cages = new List<Cage>();
        public List<Employees> employees = new List<Employees>();
        public List<Visitors> visitors = new List<Visitors>();
        public List<Cage> getCages()
        {
            return cages;
        }
        public List<Employees> getEmployees()
        {
            return employees;
        }

        public Employees getCertainEmployee(int id)
        {
            return employees[id];
        }
        public void getInfo()
        {
            int animals = 0;

            foreach (Cage cage in cages)
            {
                animals += ((ICage)cage).countAnimals();
            }

            Console.WriteLine($"Животных: {animals}.");
            Console.WriteLine($"Сотрудников: {employees.Count} .");
            Console.WriteLine($"Посетителей: {visitors.Count}.");
            Console.WriteLine($"Вольеры: {cages.Count}.");
        }
        public void caseWhenDirectorDeletesAnimal(Animal animal)
        {
            foreach (var employee in employees)
            {
                foreach(var resp in employee.responsibility)
                {
                    foreach (var beast in resp.animalsInCommon)
                    {
                        if (beast == animal)
                        {
                            employee.responsibility.Remove(resp);
                        }
                    }
                }
            }
        }
        public void addAnimal(int species)
        {
            switch (species)
            {
                case 1:
                    bool flagMonkey = true;

                    foreach (var cage in cages)
                    {
                        if ((((ICage)cage).checkSpecies() == typeof(Monkey) && ((ICage)cage).checkThreshold()) ||
                            ((ICage)cage).countAnimals() == 0)
                        {
                            flagMonkey = false;
                            ((ICage)cage).addAnimal(new Monkey());
                            ((ICage)cage).setThreshold();
                            break;
                        }
                    }
                    
                    if (flagMonkey)
                    {
                        cages.Add(new Cage());
                        ((ICage)cages.Last()).addAnimal(new Monkey());
                        ((ICage)cages.Last()).setThreshold();
                    }

                    break;
                case 2:
                    bool flagWolf = true;

                    foreach (var cage in cages)
                    {
                        if ((((ICage)cage).checkSpecies() == typeof(Wolf) && ((ICage)cage).checkThreshold()) ||
                            ((ICage)cage).countAnimals() == 0)
                        {
                            flagWolf = false;
                            ((ICage)cage).addAnimal(new Wolf());
                            ((ICage)cage).setThreshold();
                            break;
                        }
                    }

                    if (flagWolf)
                    {
                        cages.Add(new Cage());
                        ((ICage)cages.Last()).addAnimal(new Wolf());
                        ((ICage)cages.Last()).setThreshold();
                    }
                    break;
                case 3:
                    bool flagParrot = true;

                    foreach (var cage in cages)
                    {
                        if ((((ICage)cage).checkSpecies() == typeof(Parrot) && ((ICage)cage).checkThreshold()) ||
                            ((ICage)cage).countAnimals() == 0)
                        {
                            flagParrot = false;
                            ((ICage)cage).addAnimal(new Parrot());
                            ((ICage)cage).setThreshold();
                            break;
                        }
                    }

                    if (flagParrot) 
                    {
                        cages.Add(new Cage());
                        ((ICage)cages.Last()).addAnimal(new Parrot());
                        ((ICage)cages.Last()).setThreshold();
                    }

                    break;
            }
        }
        public void addVisitors()
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите пол (М | Ж): ");
            char gender = char.Parse(Console.ReadLine());

            visitors.Add(new Visitors(name, gender));
        }
        public void addEmployees()
        {
            Console.Write("Введите имя: ");
            string _name = Console.ReadLine();

            Console.Write("Введите пол (М | Ж): ");
            char _gender = char.Parse(Console.ReadLine());

            Console.WriteLine("Введите должность:\n" +
                "1. Уборщик.\n" +
                "2. Кипер.\n");
            int _occupation = int.Parse(Console.ReadLine());

            employees.Add(new Employees(_name, _gender, _occupation));
            employees.Last().getAnimalToWork(cages);
        }
        public void changeAnimal(string changeAnimal, int chosenAnimal, int numberOfCage)
        {
            switch (changeAnimal)
            {
                case "1":
                    if (cages[numberOfCage - 1].animalsInCommon[chosenAnimal].GetType() == typeof(Wolf))
                    {
                        Console.WriteLine("Это и так волк. Его менять не надо...\n");
                        return;

                    }

                    cages[numberOfCage - 1].animalsInCommon[chosenAnimal] = new Wolf();

                    Console.Write("Изменения приняты. Вот результат: ");

                    cages[numberOfCage - 1].animalsInCommon[chosenAnimal].getInfo();
                    break;
                case "2":
                    if (cages[numberOfCage - 1].animalsInCommon[chosenAnimal].GetType() == typeof(Parrot))
                    {
                        Console.WriteLine("Это и так попугай. Его менять не надо...\n");
                        return;

                    }

                    cages[numberOfCage - 1].animalsInCommon[chosenAnimal] = new Parrot();

                    Console.Write("Изменения приняты. Вот результат: ");

                    cages[numberOfCage - 1].animalsInCommon[chosenAnimal].getInfo();
                    break;
                case "3":
                    if (cages[numberOfCage - 1].animalsInCommon[chosenAnimal].GetType() == typeof(Monkey))
                    {
                        Console.WriteLine("Это и так обезьяна. Её менять не надо...\n");
                        return;

                    }

                    cages[numberOfCage - 1].animalsInCommon[chosenAnimal] = new Monkey();

                    Console.Write("Изменения приняты. Вот результат: ");

                    cages[numberOfCage - 1].animalsInCommon[chosenAnimal].getInfo();

                    break;
            }
        }
        public void removeEmployee(int index)
        {
            employees.RemoveAt(index);
        }
        public void removeVisitor(int index)
        {
            visitors.RemoveAt(index);
        }
        public void removeAnimal(int index)
        {
            ((ICage)cages).removeAnimal(index);
        }
        public void animalVoice(int index, int numberOfCage)
        {
            /*animals[index].voice();*/
            cages[numberOfCage - 1].animalsInCommon[index].voice();
        }
    }
}
