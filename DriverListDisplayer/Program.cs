using DriverListDisplayer.Interfaces;
using DriverListDisplayer.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DriverListDisplayer
{
    public class Program
    {
        static void Main(string[] args)
        {

            List<string> driveFileNames = null;
            //Get the file names from the driver file name assembly resource
            try
            {
                if (!DriverAssemblyResourceManager.GetDriverFileNames("DriverListDisplayer.Resources.DriverListFileNames.txt", Directory.GetCurrentDirectory(), out driveFileNames))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving file names - {ex.Message}");
                Console.ReadLine();
                return;
            }


            var driverRecords = new ConcurrentBag<List<IRecord>>();


            Parallel.ForEach(driveFileNames, (fileName) =>
          {
              IFileParser fileParser = new DriverFileParser();

              if (fileParser.ParseDriverFile(fileName, out var records))
              {
                  driverRecords.Add(records);
              }
          });



            //Create a list of file names




            //




        }
    }
}
