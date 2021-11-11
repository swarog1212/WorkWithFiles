using System;
using System.IO;

namespace Task3
{
    class BeforeAfterInfo
    {
        private DateTime DateTime;
        private int numFiles;
        private long size;
        public long Size 
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
        public int NumFiles
        {
            get
            {
                return numFiles;
            }
            set
            {
                numFiles = value;
            }
        }
        
        public BeforeAfterInfo(DateTime dateTime)
        {
            DateTime = dateTime;
        }
        public void DeleteFiles(DirectoryInfo directory)
        {

            foreach (var item in directory.GetFiles())
            {
                if (directory.Exists)
                {
                    if (DateTime > item.LastAccessTime)
                    {
                        item.Delete();
                    }
                }
            }
            Console.WriteLine();
        }
        public void DeleteDirectory(DirectoryInfo directory)
        {

            foreach (var item in directory.GetDirectories())
            {
                if (directory.Exists)
                {
                    if (DateTime > item.LastAccessTime)
                    {
                        item.Delete(true);
                    }
                }
            }
            Console.WriteLine();
        }

        public void OpenDirectory(DirectoryInfo directory)
        {
            if (directory.Exists)
            {
                DeleteDirectory(directory);
                DeleteFiles(directory);
                foreach (var item in directory.GetDirectories())
                {
                    OpenDirectory(item);
                }
            }
            else
            {
                Console.WriteLine("Неверно указан путь либо каталог отсутствует!");
            }
            
        }
        public long ReadeMemory(DirectoryInfo directory)
        {
            size = 0;
            if (directory.Exists)
            {
                foreach (FileInfo fi in directory.GetFiles())
                {
                    size += fi.Length;
                    numFiles++;
                } 
                
                foreach (DirectoryInfo di in directory.GetDirectories()) 
                    size += ReadeMemory(di);
            }
            else
            {
                Console.WriteLine("Неверно указан путь либо каталог отсутствует!");
            }

            return size;
        }
        
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь:");
            string newFolder = Console.ReadLine();
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            dateTime -= TimeSpan.FromMinutes(30);
            DirectoryInfo directoryInfo = new DirectoryInfo(@newFolder);
            BeforeAfterInfo delete = new BeforeAfterInfo(dateTime);
            delete.Size = delete.ReadeMemory(directoryInfo);
            Console.WriteLine($"Исходный размер папки - {delete.Size}");
            int numFiles = delete.NumFiles;
            long size = delete.Size;
            delete.OpenDirectory(directoryInfo);
            delete.Size = 0;
            delete.NumFiles = 0;
            delete.Size = delete.ReadeMemory(directoryInfo);
            numFiles -= delete.NumFiles;
            size -= delete.Size;
            Console.WriteLine($"Освобождено - {size} байт");
            Console.WriteLine($"Удалено - {numFiles} файлов");
            Console.WriteLine($"Текущий размер папки - {delete.Size} байт");
        }
    }
}
