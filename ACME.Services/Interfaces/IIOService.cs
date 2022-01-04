using System.IO;
using System.Threading.Tasks;

namespace ACME.Services.Interfaces
{
    public interface IIOService
    {
        public Task<string> ReadFile(string path);
    }
}
