using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryInfoExtender
{
    public static class DirectoryInfoExtension
    {
        public static double FilesSize(this DirectoryInfo directoryInfoObject)
        {
            long size = 0;

            foreach (var item in directoryInfoObject.GetFiles())
            {
                size += (long)item.Length;
            }

            return size;
        }
    }
}
