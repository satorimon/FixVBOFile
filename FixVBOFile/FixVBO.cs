using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FixVBOFile
{
    public class FixVBO
    {
        public static  void DoFix(string inputpath, string outputpath)
        {
            if(!File.Exists(inputpath))
            {
                throw new ArgumentException("file not found.");
            }
            if(Path.GetExtension(inputpath) != ".vbo")
            {
                throw new ArgumentException("Invalid file.");
            }
            using (var sr = new StreamReader(inputpath, Encoding.UTF8))
            using(var sw = new StreamWriter(outputpath, false, Encoding.UTF8 ))
            {
                sw.AutoFlush = true;
                var line = "";
                do
                {
                    line = sr.ReadLine();
                    sw.WriteLine(line);
                }
                while (!line.StartsWith("[column names]"));

                var columnNames = sr.ReadLine();

                var splitNames = columnNames.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var index = splitNames.Select((item, index) => (item, index))
                    .Where(pare => string.Compare(pare.item, "time", true) == 0)
                    .FirstOrDefault().index;

                sw.WriteLine(columnNames);
                do
                {
                    line = sr.ReadLine();
                    sw.WriteLine(line);
                }
                while (!line.StartsWith("[data]"));

                do
                {
                    line = sr.ReadLine();
                    var lineSplited = line.Split(" ", StringSplitOptions.None);
                    if (lineSplited.Length > index && !lineSplited[index].EndsWith('0'))
                    {
                        lineSplited[index] = lineSplited[index].Substring(0, lineSplited[index].Length - 1) + "0";
                    }
                    

                    sw.WriteLine(string.Join(" ", lineSplited));
                }
                while (!sr.EndOfStream);

                sr.Close();
                sw.Flush();
                sw.Close();

            }
        }

        
    }
}
