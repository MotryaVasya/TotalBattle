using System.IO;

namespace TotalBattle.FileSystem
{
    public class FileManager
    {
        public static string[] ReadFromFile(string filePath, 
            string fileEnding = ".txt")
        {
            string file = $"{filePath}{fileEnding}";
            if (!File.Exists(file)) return new string[0];
            return File.ReadAllLines(file);
        }

        public static void WriteToFile(string[] content, 
            string filePath, string fileEnding = ".txt")
        {
            string file = $"{filePath}{fileEnding}";
            if (!File.Exists(file)) File.Create(file).Close();
            File.WriteAllLines(file, content);
        }

        public static void DeleteFile(string filePath, string 
            fileEnding = ".txt")
        {
            string file = $"{filePath}{fileEnding}";
            if (!File.Exists(file)) return;
            File.Delete(file);
        }
    }
}
