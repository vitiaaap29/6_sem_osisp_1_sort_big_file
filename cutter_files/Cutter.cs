using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cutter_files
{
    class Cutter
    {

        struct Part
        {
            public int number;
            public int size;
            public StreamWriter stream;
            private string partName;

            public void initializeStream(string fileName)
            {
                partName = fileName + "part_" + number + ".txt";
                stream = File.CreateText(partName);
            }

            public string PartName
            {
                get 
                {
                    if (partName != null)
                        return partName;
                    else
                        return "hz_where_part";
                }
            }
        }

        public static Dictionary<String, int> DivisionFile(String filename, long maxCountChar, int sizeOneLine)
        {
            Dictionary<String, int> result = new Dictionary<string, int>();
            
            Part part = new Part();
            part.initializeStream(filename);
            string currentLine;
            StreamReader streamWriter = null;
            try
            {
                streamWriter = new StreamReader(filename);
                while ((currentLine = streamWriter.ReadLine()) != null)
                {
                    part.stream.WriteLine(currentLine);
                    part.size += sizeOneLine;
                    if (part.size > maxCountChar - sizeOneLine)
                    {
                        result.Add(part.PartName, part.size);
                        part.stream.Close();
                        part.number++;
                        part.size = 0;
                        part.initializeStream(filename);
                    }
                }

                result.Add(part.PartName, part.size);
                part.stream.Close();
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }

            return result;
        }


        /*
         * Производит слияние отсортированных файлов с именами fileNames
         * в создаваемый файл с именем resultFileName.
         */
        public static void MergeFiles(String[] fileNames, string resultFileName)
        {
            StreamWriter resultStream;

            int countFiles = fileNames.Length;
            StreamReader[] readerStreams = new StreamReader[countFiles];

            try
            {
                //открываем все файлы
                resultStream = File.CreateText(resultFileName);

                int i = 0;
                foreach (String fileName in fileNames)
                {
                    readerStreams[i++] = File.OpenText(fileName);
                }
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException("Error don't found: " + e.FileName);
            }

            int countUnMergedFiles = countFiles;
            //Получаем первые элементы каждго файла
            long[] minimums = new long[countFiles];
            for (int i = 0; i < countFiles; i++)
            {
                string line = readerStreams[i].ReadLine();

                if (line != null)
                {
                    minimums[i] = Int64.Parse(line);
                }
                else
                {
                    minimums[i] = Int64.MaxValue;
                    countUnMergedFiles--;
                }
            }

            //Слияние
            while (countUnMergedFiles > 0)
            {
                //максимальное и его индекс
                long min = minimums.Min();
                int indexMax = Array.IndexOf(minimums, min);

                //пишем минимальный в результирующий
                resultStream.WriteLine(String.Format("{0, 18}", min));
                string replacementMin = readerStreams[indexMax].ReadLine();

                if (replacementMin != null)
                {
                    minimums[indexMax] = Int64.Parse(replacementMin);
                }
                else
                {
                    minimums[indexMax] = Int64.MaxValue;
                    countUnMergedFiles--;
                }
            }

            //Закрываем все файлы
            foreach (StreamReader sr in readerStreams)
            {
                sr.Close();
            }
            resultStream.Close();
        }

    }
}
