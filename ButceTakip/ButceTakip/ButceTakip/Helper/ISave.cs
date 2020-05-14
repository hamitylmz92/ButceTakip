using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ButceTakip.Helper
{
    public interface ISave
    {
        void Save(string filename, string contentType, MemoryStream stream);
    }
}
