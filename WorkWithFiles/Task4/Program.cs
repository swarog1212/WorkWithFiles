using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task4
{
    class BinaryFileInfo 
    {  
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Симаргл\Desktop\Students.dat";
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Console.WriteLine(formatter.Deserialize(fs).ToString());
                
            }
        }
        
    }
}
