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
            DirectoryInfo di = new DirectoryInfo("C:\\Users\\pawel.lipinski\\Downloads");
            FileSystemInfo[] internals;

            try
            {
                if (di.Exists)
                {
                    internals = di.GetFileSystemInfos();

                    foreach (var item in internals)
                    {
                        if ((item.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine(item.Name);

                            if (((DirectoryInfo)item).GetFileSystemInfos().Any())
                            {
                                Console.ResetColor();

                                foreach (var element in ((DirectoryInfo)item).GetFileSystemInfos())
                                {
                                    System.Console.WriteLine("\t" + element.Name);
                                    Console.ResetColor();
                                }
                            }
                        }
                        else
                        {
                            System.Console.WriteLine(item.Name);
                        }

                        Console.ResetColor();

                    }
                }
            }
            catch
            {
                MessageBox.Show("No such directory");
            }
        }

        public bool RecursiveSubdirectoriesPrinter(FileSystemInfo analysed, int tabLength = 0)
        {
            if ((analysed.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                if (((DirectoryInfo)analysed).GetFileSystemInfos().Any())
                {
                    foreach (var item in ((DirectoryInfo)analysed).GetFileSystemInfos())
                    {
                        bool hasChildren = RecursiveSubdirectoriesPrinter(item);
                    }
                }
            }

            else
            {
                System.Console.WriteLine();
                return false;
            } 
        }
    }
}
