using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static long ReadeMemory(DirectoryInfo directory)
        {
            long size = 0;
            if (directory.Exists)
            {
                foreach (FileInfo fi in directory.GetFiles())
                {
                    try
                    {
                        size += fi.Length;
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x);
                    }
                }
                foreach (DirectoryInfo di in directory.GetDirectories())
                {
                    try
                    {
                        size += ReadeMemory(di);
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x);
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверно указан путь либо каталог отсутствует!");
            }
            
            return size;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь:");
            string newPath = Console.ReadLine();
            DirectoryInfo directoryInfo = new DirectoryInfo(@newPath);
            Console.WriteLine(ReadeMemory(directoryInfo));
        }
    }
}
