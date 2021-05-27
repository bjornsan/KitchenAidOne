using System;
using System.IO;

namespace KitchenAid.App.Services
{
    public class ErrorLogger
    {
        public void WriteToFile(string exMessage)
        {
            string date = DateTime.Now.ToString("MMMM");
            string path = GetDirectoryPath();
            string filename = $"errorlog_{date}.txt";
            string fullpath = $"{path}\\{filename}";

            using (var streamWriter = File.AppendText(fullpath))
            {
                Log(exMessage, streamWriter);
            }
        }

        private void Log(string message, TextWriter textWriter)
        {
            textWriter.WriteLine($"\r\nLog Entry: {DateTime.Now.ToLongDateString()} : {DateTime.Now.ToLongTimeString()}");
            textWriter.WriteLine("|**********|");
            textWriter.WriteLine($"    :{message}");
            textWriter.WriteLine("|**********|");
        }

        private string GetDirectoryPath()
        {
            string path = $"{Directory.GetCurrentDirectory()}\\errorlogs";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
