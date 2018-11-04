using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class FileSystem
    {
        public void ClearFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.Truncate, FileAccess.Write))
            {
                
            }
        }
    }
}
