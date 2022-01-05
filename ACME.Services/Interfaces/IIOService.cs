using System.IO;
using System.Threading.Tasks;

namespace ACME.Services.Interfaces
{
    public interface IIOService
    {
        public Task<string> ReadTextFile(string path);
        public bool ValidPath(string path);
        public string GetFilePath(string fileName);

    }
}
