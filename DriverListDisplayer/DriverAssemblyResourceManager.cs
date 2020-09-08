using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DriverListDisplayer
{
    public static class DriverAssemblyResourceManager
    {
        private const string ContainingDirectory = "Resources";
        public static bool GetDriverFileNames(string assemblyLocation, string directoryRootPath, out List<string> fileNames)
        {
            fileNames = new List<string>();
            var result = string.Empty;
            //Get the file names from the assembly
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(assemblyLocation))
            {
                if (stream == null)
                {
                    Console.WriteLine("Could not find the DriverListFileNames resource");
                    return false;
                }

                using StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
            }

            if (IsFileContentValid(result, out fileNames))
            {
                UpdateFileNames(directoryRootPath, ContainingDirectory, fileNames);
                return true;
            }
            return false;

        }


        private static void UpdateFileNames(string rootName, string containingDirectory, List<string> fileNames)
        {
            for (int i = 0; i < fileNames.Count; i++)
            {
                fileNames[i] = Path.Combine(rootName, containingDirectory, fileNames[i].Trim());
            }
        }


        public static bool IsFileContentValid(string fileContents, out List<string> fileNames)
        {
            fileNames = fileContents.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)?.ToList();
            return fileNames.Count >= 1;
        }
    }
}
