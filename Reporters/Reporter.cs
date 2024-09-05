using Test1.Reporters.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test1.Reporters
{
    public class Reporter : IReporter
    {
        private string _resultFolder = "Results";
        private const string _errorMessage = "Report error";

        public Reporter()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _resultFolder = Path.Combine(baseDirectory, "Results");

            Directory.CreateDirectory(_resultFolder);
        }

        public async Task ReportErrorAsync(int id)
        {
            await WriteToFileAsync($"Error_{id}.txt", _errorMessage);
        }

        public async Task ReportSuccessAsync(byte[] data, int id)
        {
            await WriteToFileAsync($"Success_{id}.txt", Convert.ToBase64String(data));
        }

        public async Task ReportTimeoutAsync(int id)
        {
            await WriteToFileAsync($"Timeout_{id}.txt", _errorMessage);
        }

        private async Task WriteToFileAsync(string fileName, string content)
        {
            string path = Path.Combine(_resultFolder, fileName);
            await File.WriteAllTextAsync(path, content);
            Console.WriteLine($"File created: {path}");
        }
    }
}
