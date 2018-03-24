using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectoryInfoExtender;

namespace Lab1_PT
{
    class Program
    {
        static void Main(string[] args)
        {
            //SerialiseFileCollection(args[0]);
            //List<FileSystemInfo> returnedInfo = DeserialiseFileCollection().ToList();

            //foreach (var item in returnedInfo)
            //{
            //    System.Console.WriteLine(item.Name);
            //}

            ShowSize(args[0]);
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

        public static void SerialiseFileCollection(string pathToDirectory, string pathToFile = "fileStructure.data")
        {
            DirectoryInfo di = new DirectoryInfo(pathToDirectory);
            List<FileSystemInfo> toSerialize = di.Root.EnumerateFileSystemInfos().ToList();

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = File.Create(pathToFile))
            {
                formatter.Serialize(stream, toSerialize);
            }
        }

        public static List<FileSystemInfo> DeserialiseFileCollection(string pathToFile = "fileStructure.data")
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = File.OpenRead(pathToFile))
            {
                return (List<FileSystemInfo>)formatter.Deserialize(stream);
            }
        }

        public static void ShowSize(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            double size = di.FilesSize();

            System.Console.WriteLine(size);
            
        }

    }
}
