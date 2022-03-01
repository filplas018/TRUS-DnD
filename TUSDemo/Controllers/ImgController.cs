using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUSDemo.Data;
using TUSDemo.Models;
using TUSDemo.Services;

namespace TUSDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgController : ControllerBase
    {
        private readonly FileStorageManager _fsm;
        private readonly ApplicationDbContext _context;

        public ImgController(FileStorageManager fsm, ApplicationDbContext dbContext)
        {
            _fsm = fsm;
            _context = dbContext;
        }

        //Get all files
        [HttpGet]
        public async Task<ActionResult<List<StoredFile>>> AllGet()
        {
            List<StoredFile> storedFiles = _context.Files.OrderBy(x=>x.OrderNumber).ToList();
            return storedFiles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileStream>> Get(string id)
        {
            StoredFile file = _context.Files.SingleOrDefault(x => x.StoredFileId == id);
            if (file != null)
            {
                var path = $"Files/{file.StoredFileId}";
                FileStream img = new FileStream(path, FileMode.Open);
                return Ok(img);
            }
            return BadRequest();
        }
        [HttpPut("{sourceOrder}/{destOrder}")]
        public async Task<ActionResult> ChangeOrder(int sourceOrder, int destOrder)
        {
            var sourcePic = _context.Files.Where(x=>x.OrderNumber == sourceOrder).FirstOrDefault();
            var destinationPic = _context.Files.Where(x => x.OrderNumber == destOrder).FirstOrDefault();
            if (sourcePic!=null && destinationPic!=null)
            {
                sourcePic.OrderNumber = destOrder;
                destinationPic.OrderNumber = sourceOrder;
            }
            _context.SaveChangesAsync();

            return Ok();
        }

    }
}
