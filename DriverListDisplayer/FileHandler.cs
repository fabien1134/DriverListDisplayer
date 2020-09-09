using System;
using System.IO;
using System.Reflection;

namespace DriverListDisplayer
{
    public class FileHandler
    {
        public (string fileContents, bool retrievalSucceeded) GetFileContents(string assemblyLocation)
        {
            var result = string.Empty;
            //Get the file names from the assembly
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(assemblyLocation))
            {
                if (stream == null)
                {
                    Console.WriteLine("Could not find the DriverListFileNames resource");
                    return (result, false);
                }

                using StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
            }
            return (result, true);
        }


        public (string fileContents, bool retrievalSucceeded) GetFileContents(string fileName, FileMode fileMode)
        {
            var result = string.Empty;

            using (var fileStream = new FileStream(fileName, fileMode))
            {
                if (fileStream == null)
                {
                    Console.WriteLine("Could not find the driver file");
                    return (result, false);
                }

                using var streamReader = new StreamReader(fileStream);
                result = streamReader.ReadToEnd();
            }


            return (result, true);
        }
    }
}
