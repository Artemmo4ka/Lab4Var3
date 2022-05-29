using System;
using struct_worker;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace var3_lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            Worker[] workers;
            do
            {
                Console.WriteLine("1 - записати дані у файл xml та txt.");
                Console.WriteLine("2 - читати дані з файлу txt.");
                Console.WriteLine("3 - читати дані з xml.");
                Console.WriteLine("4 - знайти працівників з txt файлу");
                Console.WriteLine("5 - знайти працівників з xml файлу");
                Console.WriteLine("0 - вихід");
                choice = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            workers = CreateAnArray();
                            WriteInTxt(workers);
                            SerializeXML(workers);
                            break;
                        case 2:
                            workers = ReadFromTxt();
                            Workers(workers);
                            break;
                        case 3:
                            workers = DeserializeXML();
                            Workers(workers);
                            break;
                        case 4:
                            FindWorker(choice);
                            break;
                        case 5:
                            FindWorker(choice);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception warning)
                {
                    Console.WriteLine("Помилка - " + warning.Message);
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (choice != 0);
        }
        static Worker[] CreateAnArray()
        {
            Console.WriteLine("Введіть кількість працівників, для заповнення:");
            int n = Convert.ToInt32(Console.ReadLine());
            Worker[] workers = new Worker[n];
            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine($"Працівник №{i + 1}:");
                workers[i] = GetWorkers();
                Console.Clear();
            }
            Array.Sort(workers);
            return workers;
        }
        static Worker GetWorkers()
        {
            Console.WriteLine("Прізвище та ініціали працівника(все разом в один рядок):");
            string surnameAndInitials = Console.ReadLine();
            Console.WriteLine("Назва займаної посади:");
            string jobTitle = Console.ReadLine();
            Console.WriteLine("Рік початку роботи:");
            int year = Convert.ToInt32(Console.ReadLine());
            return new Worker(surnameAndInitials, jobTitle,year);
        }
        static void WriteInTxt(Worker[] workers)
        {
            Console.WriteLine("Введіть назву файлу .txt:");
            string path = Console.ReadLine() + ".txt";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (Worker w in workers)
                    {
                        sw.WriteLine($"{w.surnameAndInitials}_{w.jobTitle}_{w.year}");
                    }
                }
            }
        }
        static void SerializeXML(Worker[] workers)
        {
            Console.WriteLine("Введіть назву файлу xml:");
            string path = Console.ReadLine()+".xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worker[]));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, workers);
            }
        }
        static Worker[] ReadFromTxt()
        {
            Worker[] workers;
            Console.WriteLine("Введіть назву файлу:");
            string fileName = Console.ReadLine() + ".txt";
            using (StreamReader fs = new StreamReader(fileName))
            {
                string allText = fs.ReadToEnd();
                string[] lines = allText.Split(Environment.NewLine);
                workers = new Worker[lines.Length - 1];
                for (int i = 0; i < workers.Length; i++)
                {
                    workers[i] = NewWorkerFromNewLine(lines[i]);
                }
            }
            return workers;
        }
        static Worker NewWorkerFromNewLine(string line)
        {
            string[] data = line.Split("_");
            return new Worker(data[0], data[1], Convert.ToInt32(data[2]));
        }
        static Worker[] DeserializeXML()
        {
            Worker[] workers;
            Console.WriteLine("Введіть назву файлу:");
            string path = Console.ReadLine()+".xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worker[]));
            using (StreamReader fs = new StreamReader(path))
            {
                workers = (Worker[])xmlSerializer.Deserialize(fs);
            }
            return workers;
        }
        static void Workers(Worker[] workers)
        {
            foreach (Worker w in workers)
            {
                Console.WriteLine(w.ToString());
            }
            Console.ReadKey();
        }
        static void FindWorker(int choice)
        {
            Worker[] workers;
            if (choice == 4)
            {
                workers = ReadFromTxt();
            }
            else
            {
                workers = DeserializeXML();
            }
            int thisYear = 2022;
            int result = 0;
            bool flag = false;
            Console.WriteLine("Введіть стаж роботи працівника:");
            int exp = Convert.ToInt32(Console.ReadLine());
            foreach (Worker w in workers)
            {
                result = thisYear - w.year;
                if (result > exp)
                {
                    Console.WriteLine($"{w.ToString()}");
                    flag = true;
                }
            }
            if (flag == false)
            {
                Console.WriteLine("Таких працівників немає!");
            }
        }
    }
}
