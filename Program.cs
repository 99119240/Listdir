using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Get the current user's directory
        string currentUserDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        // Check if the directory exists
        if (Directory.Exists(currentUserDirectory))
        {
            Console.WriteLine($"Scanning directory: {currentUserDirectory}");
            ScanDirectory(currentUserDirectory);
        }
        else
        {
            Console.WriteLine($"Directory does not exist: {currentUserDirectory}");
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    static void ScanDirectory(string directory)
    {
        try
        {
            // Iterate through subdirectories
            foreach (var subdirectory in Directory.EnumerateDirectories(directory))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Folder] {Path.GetFileName(subdirectory)} @ {subdirectory}");
                Console.ResetColor();

                // Recursive call to scan subdirectories
                ScanDirectory(subdirectory);
            }

            // Iterate through files
            foreach (var file in Directory.EnumerateFiles(directory))
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow; // Orange color
                    Console.WriteLine($"[File] {Path.GetFileName(file)} @ {file}");
                    Console.ResetColor();
                }
                catch (UnauthorizedAccessException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Error] {Path.GetFileName(file)} failed to read");
                    Console.ResetColor();
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error] {Path.GetFileName(directory)} failed to read");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scanning directory: {ex.Message}");
        }
    }
}
