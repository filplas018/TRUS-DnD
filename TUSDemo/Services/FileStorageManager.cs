using Microsoft.EntityFrameworkCore;
using TUSDemo.Data;
using TUSDemo.Models;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;

namespace TUSDemo.Services
{
    public class FileStorageManager
    {
        private readonly ILogger<FileStorageManager> _logger;
        private readonly ApplicationDbContext _context;
        public FileStorageManager(ILogger<FileStorageManager> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task StoreTus(ITusFile file, FileCompleteContext fileCompleteContext)
        {
            _logger.Log(LogLevel.Debug, "Storing file", file.Id);
            Dictionary<string, Metadata> metadata = await file.GetMetadataAsync(fileCompleteContext.CancellationToken);
            string? filename = metadata.FirstOrDefault(m => m.Key == "filename").Value.GetString(System.Text.Encoding.UTF8);
            string? filetype = metadata.FirstOrDefault(m => m.Key == "filetype").Value.GetString(System.Text.Encoding.UTF8);
            await CreateAsync(new StoredFile { StoredFileId = file.Id, OriginalName = filename, Uploaded = DateTime.Now, ContentType = filetype});
        }

        public async Task<ICollection<StoredFile>> ListAsync()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task<StoredFile> CreateAsync(StoredFile fileRecord)
        {
            _context.Files.Add(fileRecord);
            await _context.SaveChangesAsync();
            return fileRecord;
        }
    }
}
