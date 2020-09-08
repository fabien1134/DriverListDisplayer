using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverListDisplayer.Interfaces
{
    public interface IFileParser
    {
        bool ParseDriverFile(string fileName,out List<IRecord> records);
    }
}
