using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverListDisplayer.Model
{
    public class ColumnInfo
    {
        public int ColPosition { get; set; }
        public Type ColType { get; private set; }

        public ColumnInfo(Type colType) => ColType = colType;
    }
}
