using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverListDisplayer.Interfaces
{
    public interface IRecord
    {
        string Name { get; set; }
        int Order { get; set; }
    }
}
