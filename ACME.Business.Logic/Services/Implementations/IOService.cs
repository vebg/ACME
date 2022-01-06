using ACME.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ACME.Business.Logic.Services.Implementations
{
    public class IOService : IIOService
    {
        private readonly IIOService iOService;

        public string GetFilePath(string fileName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string absolutePath = $"{projectDirectory}\\inputs\\{fileName}";
            return absolutePath;
        }

        public async Task<string> ReadTextFile(string path)
        {
           return await File.ReadAllTextAsync(path);
        }

        public bool ValidPath(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }
    }
}
