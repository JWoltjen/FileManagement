using System;
using System.IO;

namespace FileManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            //  the current working directory of the application is set to the bin\Debug\net6.0 folder within your project, not the project folder itself. 
            string projectDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\")); // Go up three levels to the project folder
            string startFolder = Path.Combine(projectDirectory, "StartFolder");
            string endFolder = Path.Combine(projectDirectory, "EndFolder");

            CopyFilesRecursively(startFolder, endFolder);
            Console.WriteLine("Files copied successfully!");
        }

        public static void CopyFilesRecursively(string sourcePath, string destinationPath)
        {
            // Create the destination directory if it doesn't exist
            Directory.CreateDirectory(destinationPath);

            // Copy all files from the source to the destination directory
            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string fileName = Path.GetFileName(file);
                string destinationFile = Path.Combine(destinationPath, fileName);

                // Only copy if the file doesn't already exist in the destination
                if (!File.Exists(destinationFile))
                {
                    File.Copy(file, destinationFile);
                }
            }

            // Recursively copy all subdirectories
            foreach (string directory in Directory.GetDirectories(sourcePath))
            {
                string directoryName = Path.GetFileName(directory);
                string destinationDirectory = Path.Combine(destinationPath, directoryName);

                CopyFilesRecursively(directory, destinationDirectory);
            }
        }
    }
}
