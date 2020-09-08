using DriverListDisplayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverListDisplayer.Model
{
    public class DriverRecord : IRecord
    {
        public string Team { get; set; }
        public string Name { get; set; }
        public int RaceNumber { get; set; }
        public int Order { get; set; }



        public void UpdateAppropriateCol(string colHeader, string colValue)
        {

            switch (colHeader)
            {
                case "Team":
                    Team = colValue.Trim();
                    break;
                case "Name":
                    Name = colValue?.Trim();
                    break;
                case "Race Number":
                    if (!int.TryParse(colValue, out int raceNumber)) return;
                    RaceNumber = raceNumber;
                    break;
                case "Order":
                    if (!int.TryParse(colValue, out int orderNumber)) return;
                    Order = orderNumber;
                    break;
            }
        }



    }
}
