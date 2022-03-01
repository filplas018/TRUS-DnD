using System.ComponentModel.DataAnnotations;

namespace TUSDemo.Models
{
    public class StoredFile
    {
        [Key]
        public string StoredFileId { get; set; }
        [Required]
        public string OriginalName { get; set; } = "";
        public int OrderNumber { get; set; }
        public string ContentType { get; set; } = "";
        public DateTime Uploaded { get; set; }
        public long Size { get; set; }

    }
}
