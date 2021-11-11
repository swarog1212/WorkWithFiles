using System;
using System.IO;

namespace Task1
{
    class DeleteLastAccTime
    {
        private DateTime DateTime;
        public DeleteLastAccTime(DateTime dateTime)
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
        }

        public void OpenDirectory(DirectoryInfo directory)
        {
            DeleteDirectory(directory);
            DeleteFiles(directory);
            foreach (var item in directory.GetDirectories())
            {
                OpenDirectory(item);
            }
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
            DeleteLastAccTime deleteDir = new DeleteLastAccTime(dateTime);
            deleteDir.OpenDirectory(directoryInfo);
            
        }
    }
}
