using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zoo
{
    class DIRECTOR
    {
        static CancellationTokenSource cancellationTokenSource;
        static string command = "МЕНЮ";
        static Zoo zoo = new Zoo();
        static async Task timer()
        {
            CancellationToken token = cancellationTokenSource.Token;

            while (!token.IsCancellationRequested)
            {
                for (int i = 0; i < zoo.cages.Count; i++)
                {
                    List<Animal> beasts = zoo.cages[i].animalsInCommon;
                    foreach (var animal in beasts)
                    {
                        animal.gettingHungry(zoo.getCages(), i);
                    }
                }
                foreach (var employee in zoo.employees)
                {
                    employee.getFeeded();
                }

                for (int i = 0; i < zoo.cages.Count; i++)
                {
                    foreach (var visitors in zoo.visitors)
                    {
                        Random random = new Random();

                        Cage cage = zoo.cages[i];

                        int randomizedAnimal = random.Next(0, cage.getAnimalsAmount());

                        List<Animal> animalsInCommon = cage.getAnimalsInCommon();

                        Animal animal = animalsInCommon[randomizedAnimal];

                        visitors.feedAnimal(cage, animal);
                    }
                }

                await Task.Delay(800);
            }
        }
        static async Task movingFromOpenedToClosed()
        {
            Random rand = new Random();

            while (true)
            {
                foreach (var cage in zoo.cages)
                {
                    for (int i = 0; i < cage.animalsInCommon.Count; i += rand.Next(1, 8))
                    {
                        if (i < cage.animalsInCommon.Count)
                        {
                            var animal = cage.animalsInCommon[i];
                            if (cage.animalsOpened.Contains(animal))
                            {
                                ((ICageOpened)cage.animalsInCommon[i]).moveToClosed(cage.animalsInCommon[i]);
                            }
                            else if (cage.animalsClosed.Contains(animal))
                            {
                                ((ICageClosed)cage.animalsInCommon[i]).moveToOpened(cage.animalsInCommon[i]);
                            }
                        }

                    }
                }
                await Task.Delay(rand.Next(7000, 15000));
            }
        }
        static void randomAnimalsInCages()
        {
            Random rand = new Random();

            int numberOfAnimals = rand.Next(15, 31) + 1;

            for (int i = 0; i < numberOfAnimals; i++)
            {
                int species = rand.Next(0, 3) + 1;
                zoo.addAnimal(species);
            }
        }
        static void Main(string[] args)
        {
            randomAnimalsInCages();
            movingFromOpenedToClosed();
            Console.WriteLine("Команды выделены цифрами. Программа начинается.\n");

            while (command != "6")
            {
                Console.WriteLine("1. Добавить.\n" +
                                  "2. Редактировать.\n" +
                                  "3. Удалить.\n" +
                                  "4. Проверить статус.\n" +
                                  "5. Приказать подать голос.\n" +
                                  "6. Завершить программу.\n" +
                                  "7. Взаимодействие с таймером.\n");
                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        add();
                        break;
                    case "2":
                        edit();
                        break;
                    case "3":
                        remove();
                        break;
                    case "4":
                        checkInfo();
                        break;
                    case "5":
                        order();
                        break;
                    case "7":
                        timeCommander();
                        break;
                }
            }

            Console.WriteLine("Программа завершила свою работу. Нажмите любую клавишу, чтоб выйти...");
            Console.ReadLine();
        }
        static void add()
        {
            while (true)
            {
                Console.WriteLine("Кого вы хотите добавить? Либо введите 0, чтобы выйти в главное меню.\n" +
                "1. Посетителя.\n" +
                "2. Сотрудника.\n" +
                "3. Животное.\n" +
                "4. Вольер.\n");


                int index = int.Parse(Console.ReadLine());

                switch (index)
                {
                    case 0:
                        Console.WriteLine("Вы перебрасываетесь в главное меню.\n");
                        return;
                    case 1:
                        zoo.addVisitors();
                        break;
                    case 2:
                        zoo.addEmployees();
                        break;
                    case 3:
                        if (zoo.cages.Count == 0)
                        {
                            Console.WriteLine("Вольеров нет. Создадите сначала вольер...\n");
                            return;
                        }

                        Console.WriteLine("Выберите вольер.\n");

                        for (int i = 0; i < zoo.cages.Count; i++)
                            Console.WriteLine($"{i + 1} вольер.");

                        int numberOfCage = int.Parse(Console.ReadLine());

                        Console.WriteLine("Какое животное вы хотите добавить?\n" +
                            "1. Обезьяну.\n" +
                            "2. Волка.\n" +
                            "3. Попугая.\n");

                        int number = int.Parse(Console.ReadLine());
                        addAnimal(number, numberOfCage);
                        break;
                    case 4:
                        zoo.cages.Add(new Cage());
                        Console.WriteLine("Вольер успешно создан.\n");
                        break;
                }
            }
        }
        static void addAnimal(int number, int numberOfCage)
        {
            zoo.addAnimal(number);
        }
        static void editVisitor(int index)
        {
            int number = int.Parse(Console.ReadLine());

            switch (number)
            {
                case 1:
                    Console.Write("Введите новое имя: ");
                    zoo.visitors[index].changeName();
                    break;
                case 2:
                    Console.Write("Введите изменённый пол (М | Ж): ");
                    zoo.visitors[index].changeGender();
                    break;
            }

            Console.WriteLine("Изменения применены. Статус измененного посетителя:\n");
            zoo.visitors[index].getInfo();
        }
        static void editAnimal(int chosenAnimal, int numberOfCage)
        {
            int number = int.Parse(Console.ReadLine());

            switch (number)
            {
                case 1:
                    Console.WriteLine("На какой вид вы его хотите поменять? Выберите тот же вид, если хотите оставить без изменений.\n" +
                        "1. Волк.\n" +
                        "2. Попугай.\n" +
                        "3. Обезьяна.");
                    string changeAnimal = Console.ReadLine();
                    zoo.changeAnimal(changeAnimal, chosenAnimal, numberOfCage);

                    break;
                case 2:
                    Console.WriteLine("Введите значение сытости, которое вы хотите установить для животного.\n");
                    int value = int.Parse(Console.ReadLine());
                    zoo.cages[numberOfCage - 1].animalsInCommon[chosenAnimal].changeState(value);

                    Console.Write("Изменения приняты. Вот результат: ");
                    zoo.cages[numberOfCage - 1].animalsInCommon[chosenAnimal].getInfo();

                    break;

            }
        }
        static void editEmployee(int index)
        {
            int number = int.Parse(Console.ReadLine());

            Employees employee = zoo.getCertainEmployee(index);
            List<Cage> cages = zoo.getCages();


            switch (number)
            {
                case 1:
                    Console.Write("Введите новое имя: ");
                    employee.changeName();
                    break;
                case 2:
                    Console.Write("Введите изменённый пол (М | Ж): ");
                    employee.changeGender();
                    break;
                case 3:
                    Console.Write("Введите новую должность:\n" +
                        "1. Уборщик.\n" +
                        "2. Кипер.\n");
                    int occupation = int.Parse(Console.ReadLine());

                    switch (occupation)
                    {
                        case 1:
                            if (employee.occupation == 1)
                            {
                                Console.WriteLine("Он и так уже уборщик...\n" +
                                    "Вы перебрасываетесь в меню редактирования.\n");
                                return;
                            }

                            employee.changeOccupation(1);
                            break;
                        case 2:
                            if (employee.occupation == 2)
                                Console.WriteLine("В таком случае назначьте новый список животных для сотрудника:\n");

                            employee.changeOccupation(2);
                            employee.getAnimalToWork(cages);
                            break;
                    }

                    break;
            }

            Console.WriteLine("Изменения применены. Статус измененного сотрудника:\n");
            employee.getInfo();
        }
        static void edit()
        {
            while (true)
            {
                Console.WriteLine("Введите, кого вы хотите редактировать. Либо введите 0, чтоб вернуться в главное меню.\n" +
                "1. Посетителя.\n" +
                "2. Сотрудника.\n" +
                "3. Животное.\n");
                int number = int.Parse(Console.ReadLine());

                switch (number)
                {
                    case 0:
                        Console.WriteLine("Вы перебрасываетесь в главное меню.\n");
                        return;
                    case 1:
                        if (zoo.visitors.Count == 0)
                        {
                            Console.WriteLine("Нет ни одного посетителя...\n");
                            break;
                        }

                        Console.WriteLine("Вот список посетителей. Выберите номер того, кого вы хотите редактировать.\n");

                        for (int i = 0; i < zoo.visitors.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            zoo.visitors[i].getInfo();
                        }

                        int chosenVisitor = int.Parse(Console.ReadLine());

                        Console.WriteLine("\nЧто вы хотите изменить?\n" +
                            "1. Имя.\n" +
                            "2. Пол.\n");

                        editVisitor(chosenVisitor - 1);
                        break;
                    case 2:
                        if (zoo.employees.Count == 0)
                        {
                            Console.WriteLine("Нет ни одного сотрудника...\n");
                            break;
                        }

                        Console.WriteLine("Вот список сотрудников. Выберите номер того, кого вы хотите редактировать.\n");

                        for (int i = 0; i < zoo.employees.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            zoo.employees[i].getInfo();
                        }

                        int chosenEmlpoyee = int.Parse(Console.ReadLine());

                        Console.WriteLine("\nЧто вы хотите изменить?\n" +
                            "1. Имя.\n" +
                            "2. Пол.\n" +
                            "3. Должность.\n");

                        editEmployee(chosenEmlpoyee - 1);
                        break;
                    case 3:
                        bool _isEmply = true;
                        List<Cage> cages = zoo.getCages();

                        foreach (var cage in cages)
                        {
                            if (cage.animalsInCommon.Count > 0)
                            {
                                _isEmply = false;
                                break;
                            }
                        }

                        if (_isEmply)
                        {
                            Console.WriteLine("Нет ни одного животного...\n");
                            break;
                        }

                        Console.WriteLine("Список вольеров. Выберите номер:");

                        for (int i = 0; i < cages.Count; i++)
                            Console.WriteLine($"{i + 1} вольер.");

                        int numberOfCage = int.Parse(Console.ReadLine());

                        Console.WriteLine("Вот список животных. Выберите номер того, кого вы хотите редактировать.\n");


                        int amountOfCages = cages[numberOfCage - 1].getAnimalsAmount();

                        for (int i = 0; i < amountOfCages; i++)
                        {
                            Console.Write($"{i + 1}. ");

                            Animal animal = cages[numberOfCage - 1].animalsInCommon[i];

                            animal.getInfo();
                        }

                        int chosenAnimal = int.Parse(Console.ReadLine()) - 1;

                        Console.WriteLine("\nЧто вы хотите поменять?\n" +
                            "1. Вид.\n" +
                            "2. Значение сытости.\n");

                        editAnimal(chosenAnimal, numberOfCage);
                        break;
                }
            }
        }
        static void remove()
        {
            while (true)
            {
                Console.WriteLine("Выберите кого удалить. Либо вернитесь в главное меню, нажав на 0.\n" +
                "1. Сотрудника.\n" +
                "2. Посетителя.\n" +
                "3. Животное.\n");

                int num = int.Parse(Console.ReadLine());

                List<Cage> cages = zoo.getCages();

                switch (num)
                {
                    case 0:
                        Console.WriteLine("Вы перебрасываетесь в главное меню.\n");
                        return;
                    case 1:
                        if (zoo.employees.Count == 0)
                        {
                            Console.WriteLine("Нет ни одного сотрудника...\n");
                            break;
                        }

                        for (int i = 0; i < zoo.employees.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            zoo.employees[i].getInfo();
                        }

                        Console.Write("Введите номер того, кого вы хотите удалить: ");
                        int number = int.Parse(Console.ReadLine());
                        zoo.removeEmployee(number - 1);
                        Console.WriteLine($"Сотрудник под номером {number} был успешно удалён.\n");
                        break;
                    case 2:
                        if (zoo.visitors.Count == 0)
                        {
                            Console.WriteLine("Нет ни одного посетителя...\n");
                            break;
                        }

                        for (int i = 0; i < zoo.visitors.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            zoo.visitors[i].getInfo();
                        }

                        Console.Write("Введите номер того, кого вы хотите удалить: ");
                        int _number = int.Parse(Console.ReadLine());
                        zoo.removeVisitor(_number - 1);
                        Console.WriteLine($"Посетитель под номером {_number} был успешно удалён.\n");
                        break;
                    case 3:
                        bool _isEmply = true;
                        foreach (var cage in cages)
                        {
                            if (cage.animalsInCommon.Count > 0)
                            {
                                _isEmply = false;
                                break;
                            }
                        }

                        if (_isEmply)
                        {
                            Console.WriteLine("Нет ни одного животного...\n");
                            break;
                        }

                        Console.WriteLine("Список вольеров. Выберите номер:");

                        for (int i = 0; i < cages.Count; i++)
                            Console.WriteLine($"{i + 1} вольер.");

                        int numberOfCage = int.Parse(Console.ReadLine());

                        Console.WriteLine("Вот список животных. Выберите номер того, кого вы хотите удалить.\n");

                        for (int i = 0; i < cages[numberOfCage - 1].animalsInCommon.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");

                            Cage chosenCage = cages[numberOfCage - 1];

                            List<Animal> animalsInCommon  = chosenCage.getAnimalsInCommon();

                            animalsInCommon[i].getInfo();
                        }

                        int species = int.Parse(Console.ReadLine()) - 1;

                        int amountOfCages = cages[numberOfCage - 1].getAnimalsAmount();

                        Cage CertainCage = cages[numberOfCage - 1];

                        Animal animal = CertainCage.getCertainAnimal(species);

                        zoo.caseWhenDirectorDeletesAnimal(animal);
                        Console.WriteLine($"Животное под номером {species} было успешно удалено.\n");
                        break;
                }
            }
        }
        static void checkInfo()
        {
            while (true)
            {
                Console.WriteLine("Проверить статус:\n" +
                "\n1. Сотрудника" +
                "\n2. Посетителя" +
                "\n3. Животного" +
                "\n4. Зоопарка" +
                "\n5. Выйти в главное меню" +
                "\n6. Вольера");

                command = Console.ReadLine();
                Console.WriteLine();

                switch (command)
                {
                    case "1":
                        if (zoo.employees.Count == 0)
                        {
                            Console.WriteLine("Нет ни одного работника...\n");
                            break;
                        }

                        for (int i = 0; i < zoo.employees.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            zoo.employees[i].getInfo();
                        }
                        break;
                    case "2":
                        if (zoo.visitors.Count == 0)
                        {
                            Console.WriteLine("Нет ни одного посетителя...\n");
                            break;
                        }

                        for (int i = 0; i < zoo.visitors.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            zoo.visitors[i].getInfo();
                        }
                        break;
                    case "3":
                        bool _isEmply = true;
                        foreach (var cage in zoo.cages)
                        {
                            if (cage.animalsInCommon.Count > 0)
                            {
                                _isEmply = false;
                                break;
                            }
                        }

                        if (_isEmply)
                        {
                            Console.WriteLine("Нет ни одного животного...\n");
                            break;
                        }

                        Console.WriteLine("Список вольеров. Выберите номер:");

                        for (int i = 0; i < zoo.cages.Count; i++)
                            Console.WriteLine($"{i + 1} вольер.");

                        int numberOfCage = int.Parse(Console.ReadLine());

                        Console.WriteLine("Вот список животных.\n");

                        for (int i = 0; i < zoo.cages[numberOfCage - 1].animalsInCommon.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            zoo.cages[numberOfCage - 1].animalsInCommon[i].getInfo();
                        }

                        break;
                    case "4":
                        zoo.getInfo();
                        break;
                    case "5":
                        Console.WriteLine("Вы перебрасываетесь в главное меню.");
                        Console.WriteLine();
                        return;

                    case "6":
                        Console.WriteLine("О каком вальере вы хотите узнать информацию?\n");

                        for (int i = 0; i < zoo.cages.Count; i++)
                            Console.WriteLine($"{i + 1} вольер.");

                        int _numberOfCage = int.Parse(Console.ReadLine());

                        ((ICage)zoo.cages[_numberOfCage - 1]).getSlightInfoForDirector();


                        break;
                }
                Console.WriteLine();
            }
        }
        static void order()
        {
            bool _isEmply = true;
            foreach (var cage in zoo.cages)
            {
                if (cage.animalsInCommon.Count > 0)
                {
                    _isEmply = false;
                    break;
                }
            }

            if (_isEmply)
            {
                Console.WriteLine("Нет ни одного животного...\n");
                return;
            }

            Console.WriteLine("Список вольеров. Выберите номер:");

            for (int i = 0; i < zoo.cages.Count; i++)
                Console.WriteLine($"{i + 1} вольер.");

            int numberOfCage = int.Parse(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("Какому животному вы хотите подать команду? Введите 0, если хотите вернуться в главное меню.\n");

                Console.WriteLine("Вот список животных.\n");

                for (int i = 0; i < zoo.cages[numberOfCage - 1].animalsInCommon.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    zoo.cages[numberOfCage - 1].animalsInCommon[i].getInfo();
                }

                int index = int.Parse(Console.ReadLine()) - 1;
                if (index < 0)
                {
                    Console.WriteLine("Вы перебрасываетесь в главное меню.\n");
                    return;
                }
                zoo.animalVoice(index, numberOfCage);
            }
        }
        static void timeCommander()
        {
            byte digital = 0;

            while (digital != 1 && digital != 2)
            {
                Console.WriteLine("Что вы хотите сделать?\n" +
                "1. Запустить таймер.\n" +
                "2. Остановить таймер.\n");

                digital = byte.Parse(Console.ReadLine());

                if (digital == 1)
                {
                    if (cancellationTokenSource != null) cancellationTokenSource.Cancel();

                    cancellationTokenSource = new CancellationTokenSource();
                    timer();
                }
                else if (digital == 2)
                {
                    stopTime();
                }
            }
        }
        static void stopTime()
        {
            cancellationTokenSource.Cancel();
        }
    }
}