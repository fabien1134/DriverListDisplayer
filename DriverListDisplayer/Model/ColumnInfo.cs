using System;

namespace DriverListDisplayer.Model
{
    public class ColumnInfo
    {
        public int ColPosition { get; set; }
        public Type ColType { get; private set; }

        public ColumnInfo(Type colType) => ColType = colType;
    }
}
