using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DriverListDisplayer
{
    public class DriverAssemblyResourceManager
    {
        private const string ContainingDirectory = "Resources";
        private readonly FileHandler _fileHandler;
        public DriverAssemblyResourceManager(FileHandler fileHandler) => _fileHandler = fileHandler;


        public bool GetDriverFileNames(string assemblyLocation, string directoryRootPath, out List<string> fileNames)
        {
            fileNames = new List<string>();

            var (fileContents, retrievalSucceeded) = _fileHandler.GetFileContents(assemblyLocation);

            if (!retrievalSucceeded) return false;

            if (IsFileContentValid(fileContents, out fileNames))
            {
                UpdateFileNames(directoryRootPath, ContainingDirectory, fileNames);
                return true;
            }
            return false;

        }


        private void UpdateFileNames(string rootName, string containingDirectory, List<string> fileNames)
        {
            for (int i = 0; i < fileNames.Count; i++)
            {
                fileNames[i] = Path.Combine(rootName, containingDirectory, fileNames[i].Trim());
            }
        }


        public bool IsFileContentValid(string fileContents, out List<string> fileNames)
        {
            fileNames = fileContents.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)?.ToList();
            return fileNames.Count >= 1;
        }
    }
}
