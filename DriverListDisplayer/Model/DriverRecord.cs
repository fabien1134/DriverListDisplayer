using DriverListDisplayer.Interfaces;

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


            if (RecordFields.Team == colHeader)
            {
                  Team = colValue.Trim();
            }
            else if (RecordFields.Name == colHeader)
            {
                  Name = colValue?.Trim();
            }
            else if(RecordFields.RaceNumber == colHeader)
            {
                if (!int.TryParse(colValue, out int raceNumber)) return;
                RaceNumber = raceNumber;
            }
            else if(RecordFields.Order == colHeader)
            {
                if (!int.TryParse(colValue, out int orderNumber)) return;
                Order = orderNumber;
            }    
        }
    }
}
