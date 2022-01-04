using ACME.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ACME.Business.Logic.Services.Implementations
{
    public class IOService : IIOService
    {
        public async Task<string> ReadFile(string path)
        {
            if(File.Exists(path))
            {
                return await File.ReadAllTextAsync(path);
            }
            throw new FileNotFoundException(Constants.ErrorMessages.FILE_NOT_FOUND_TRY_AGAIN);
        }
    }
}
