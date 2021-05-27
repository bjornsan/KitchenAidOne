using System;
using System.IO;

namespace KitchenAid.App.Services
{
    /// <summary>Class used to log errors and exceptions to file.</summary>
    public class ErrorLogger
    {
        /// <summary>Writes the error/ exception message to file.</summary>
        /// <param name="exMessage">The ex message.</param>
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

        /// <summary>Logs the specified message.</summary>
        /// <param name="message">The message.</param>
        /// <param name="textWriter">The text writer.</param>
        private void Log(string message, TextWriter textWriter)
        {
            textWriter.WriteLine($"\r\nLog Entry: {DateTime.Now.ToLongDateString()} : {DateTime.Now.ToLongTimeString()}");
            textWriter.WriteLine("|**********|");
            textWriter.WriteLine($"    :{message}");
            textWriter.WriteLine("|**********|");
        }

        /// <summary>Gets the directory path.</summary>
        /// <returns>
        ///   The path as a string.
        /// </returns>
        private string GetDirectoryPath()
        {
            string path = $"{Directory.GetCurrentDirectory()}\\errorlogs";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
