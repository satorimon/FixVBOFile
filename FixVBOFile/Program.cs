using System;
using System.IO;

namespace FixVBOFile
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("引数が有りません。処理を終了します。");
                return;
            }
            foreach (var s in args)
            {
                Console.WriteLine(s);
                var inputPath = s;
                var filename = Path.GetFileNameWithoutExtension(inputPath);
                filename += "_fixed";
                var outputPath = Path.GetDirectoryName(inputPath) + Path.DirectorySeparatorChar
                    + filename + Path.GetExtension(inputPath);

                FixVBO.DoFix(inputPath, outputPath);
            }




        }
    }
}
