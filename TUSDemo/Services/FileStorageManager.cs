using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;

namespace TUSDemo.Services
{
    public class FileStorageManager
    {
        public async Task StoreTus(ITusFile file, FileCompleteContext fileCompleteContext)
        {
            Dictionary<string, Metadata> metadata = await file.GetMetadataAsync(fileCompleteContext.CancellationToken);

        }
    }
}
