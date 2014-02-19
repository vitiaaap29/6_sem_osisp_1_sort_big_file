using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cutter_files
{
    class SorterBigFiles
    {
        public static void Sort(string fileName, int maxSizePart, int lengthLine)
        {
            DateTime beforeDivision = DateTime.Now;
            //Делим на кусочки
            Console.WriteLine("Division file...");
            Dictionary<string, int> filesAndSizes = Cutter.DivisionFile(fileName, maxSizePart, lengthLine);
            DateTime afterDivision = DateTime.Now;

            DateTime beforeSort = DateTime.Now;
            //Сортируем кусочкм
            Console.WriteLine("Sort parts");
            foreach (KeyValuePair<string, int> fileSizePair in filesAndSizes)
            {
                Sorting.SortFile(fileSizePair.Key, fileSizePair.Value / lengthLine);
            }
            DateTime afterSort = DateTime.Now;

            File.Delete(fileName);

            DateTime beforeMerge = DateTime.Now;
            Console.WriteLine("Merge files...");
            Cutter.MergeFiles(filesAndSizes.Keys.ToArray(), fileName);
            DateTime afterMerge = DateTime.Now;

            //Удаляем кусочки
            Console.WriteLine("Delete Parts");
            foreach (KeyValuePair<string, int> filenameSizePair in filesAndSizes)
            {
                if (File.Exists(filenameSizePair.Key))
                {
                    File.Delete(filenameSizePair.Key);
                }
            }

            Console.WriteLine("beforeDivision => " + beforeDivision);
            Console.WriteLine("afterDivision => " + afterDivision);
            Console.WriteLine("beforeSort => " + beforeSort);
            Console.WriteLine("afterSort => " + afterSort);
            Console.WriteLine("beforeMerge => " + beforeMerge);
            Console.WriteLine("afterMerge => " + afterMerge);
        }
    }
}
