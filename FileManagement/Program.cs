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
            // destinationPath might be something like "C:\EndFolder".
            // If the directory does already exist, this method does nothing, so it's safe to call even if you're not sure whether the directory is there.
            Directory.CreateDirectory(destinationPath);

            // Copy all files from the source to the destination directory
            // file might be something like "C:\StartFolder\textFile1.txt".
            foreach (string file in Directory.GetFiles(sourcePath))
            {
                // By using Path.GetFileName(file), you get "textFile1.txt"
                string fileName = Path.GetFileName(file);
                // and then Path.Combine(destinationPath, fileName) gives you "C:\EndFolder\textFile1.txt".
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
