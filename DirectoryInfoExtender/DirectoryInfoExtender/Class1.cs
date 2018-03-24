using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryInfoExtender
{
    public static class Class1
    {
        public static long FilesSize(this string path)
        {
            long size = 0;

            DirectoryInfo di = new DirectoryInfo(@path);

            foreach (var item in di.GetFiles())
            {
                size += (long)item.Length;
            }

            return size;
        }
    }
}
