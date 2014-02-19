using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.IO.StreamWriter;

namespace cutter_files 
{ 
    class Sorting
    {
        //countOfItems - количество строк
        public static void SortFile(string filePath, int countOfItems)
        {
            long[] array = FileToArray(filePath, countOfItems);
            SortArray(array);
            WriteFile(array, filePath);
            Console.WriteLine("zer good.");
        }

        private static long[] FileToArray(string filePath, int countOfItems)
        {
            Console.WriteLine("FileToArray started");
            long[] array = new long[countOfItems];
            string line; 
            int index = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);      
            while ((line = file.ReadLine()) != null) 
            { 
                array[index] = Convert.ToInt64(line);                
                /*if((index % 100000) == 0)
                {  
                     System.Console.WriteLine(array[index]);
                }*/
                index++;  
            }
            file.Close();
            Console.WriteLine("FileToArray completed");
            return array;
        }

        private static long[] SortArray(long[] array)
        {
            Console.WriteLine("SortArray started:)");
            Array.Sort(array);
            Console.WriteLine("SortArray completed");
            return array;            
        }

        private static void WriteFile(long[] array, string filePath)
        {
            //удаляем исходный файл
            Console.WriteLine("WriteFile started");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            //пишем в новый файл
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
            foreach (long tempValue in array)
            {
                file.WriteLine(String.Format("{0, 18}", tempValue));
            }
            file.Close();
            Console.WriteLine("WriteFile completed");
        }
    }
}
