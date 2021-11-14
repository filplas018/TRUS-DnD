using Microsoft.AspNetCore.Mvc;
using TUSDemo.Models;
using TUSDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TUSDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileStorageManager _fsm;

        public FilesController(FileStorageManager fsm)
        {
            _fsm = fsm;
        }

        // GET: api/<FilesController>
        [HttpGet]
        public async Task<IEnumerable<StoredFile>> Get()
        {
            return await _fsm.ListAsync();
        }

        // GET api/<FilesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FilesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FilesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FilesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
