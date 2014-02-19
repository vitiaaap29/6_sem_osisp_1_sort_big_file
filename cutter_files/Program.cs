using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cutter_files
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName;
            Console.WriteLine("Type filename");
            fileName = Console.ReadLine();
            Console.WriteLine("Type maxPartSize");
            int maxSizePart = Int32.Parse(Console.ReadLine());

            SorterBigFiles.Sort(fileName, maxSizePart, 20);

            Console.ReadKey();
        }
    }
}
