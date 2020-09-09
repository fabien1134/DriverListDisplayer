using DriverListDisplayer.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriverListDisplayer
{
    public class DriverListProceser
    {
        public string ProcessList(ConcurrentBag<List<IRecord>> driverRecords)
        {
            if(driverRecords==null || driverRecords.Count == 0)
            {
                throw new Exception("No driver records to process");
            }

            var orderedList = new List<IRecord>();
            var output = new StringBuilder();

            foreach (var driverRecord in driverRecords)
            {
                orderedList.AddRange(driverRecord);
            }

            var orderedDriverList = orderedList.OrderBy(record => record.Order).Select(record => (record.Order, record.Name)).ToList();

            orderedDriverList.ForEach(driverDetail => output.AppendLine($"{driverDetail.Name} {driverDetail.Order}")  );

            return output.ToString();

        }
    }
}
