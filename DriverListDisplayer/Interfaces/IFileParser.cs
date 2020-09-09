using System.Collections.Generic;

namespace DriverListDisplayer.Interfaces
{
    public interface IFileParser
    {
        bool ParseDriverFile(string fileName,out List<IRecord> records);
    }
}
