using DriverListDisplayer.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DriverListDisplayer
{
    public class Program
    {
        static void Main(string[] args)
        {

            List<string> driveFileNames = null;
            var fileHandler = new FileHandler();
            var driverAssemblyResourceManager = new DriverAssemblyResourceManager(fileHandler);
            //Get the file names from the driver file name assembly resource
            try
            {
                if (!driverAssemblyResourceManager.GetDriverFileNames("DriverListDisplayer.Resources.DriverListFileNames.txt", Directory.GetCurrentDirectory(), out driveFileNames))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Error retrieving file names - {ex.Message}");
                return;
            }


            var driverRecords = new ConcurrentBag<List<IRecord>>();

            try
            {
                Parallel.ForEach(driveFileNames, (fileName) =>
                   {
                       IFileParser fileParser = new DriverFileParser(fileHandler);

                       if (fileParser.ParseDriverFile(fileName, out var records))
                       {
                           driverRecords.Add(records);
                       }
                   });
            }
            catch (Exception e)
            {
                DisplayErrorMessage("Issue parsing file" + e.Message);
                return;
            }



            var driverList = new DriverListProceser();

            try
            {
                string orderedDriverList = driverList.ProcessList(driverRecords);
                Console.WriteLine(orderedDriverList);
            }
            catch (Exception e)
            {
                DisplayErrorMessage("Issue processing list" + e.Message);
                return;
            }

            //Keep the output displayed
            Console.ReadLine();
        }


        private static void DisplayErrorMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Console.ReadLine();
        }
    }
}
