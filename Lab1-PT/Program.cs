using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_PT
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryPresenter("C:\\Users\\pawel.lipinski\\Downloads");
        }

        public static bool RecursiveSubdirectoriesPrinter(FileSystemInfo analysed, int tabLength = 0)
        {
            bool hasChildren = false;

            if ((analysed.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(new String('\t', tabLength) + analysed.Name);
                Console.ResetColor();

                if (((DirectoryInfo)analysed).GetFileSystemInfos().Any())
                {
                    foreach (var item in ((DirectoryInfo)analysed).GetFileSystemInfos())
                    {
                        hasChildren = RecursiveSubdirectoriesPrinter(item, tabLength+1);
                    }
                }
            }

            else
            {
                System.Console.WriteLine(new String('\t', tabLength) + analysed.Name);
            }

            return hasChildren;
        }

        public static void DirectoryPresenter(string pathToDirectory)
        {
            DirectoryInfo di = new DirectoryInfo(pathToDirectory);
            FileSystemInfo[] internals;

            try
            {
                if (di.Exists)
                {
                    internals = di.GetFileSystemInfos();

                    foreach (var item in internals)
                    {
                        RecursiveSubdirectoriesPrinter(item);
                    }

                }
            }
            catch
            {
                MessageBox.Show("No such directory");
            }
        }
    }
}
