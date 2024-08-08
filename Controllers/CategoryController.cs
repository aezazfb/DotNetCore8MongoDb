using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using testProjectApis.Models;
using testProjectApis.services;

namespace testProjectApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMongoCollection<Category>? _categories;
        private readonly IMongoCollection<FileModel>? _files;
        private readonly TokenService _tokenService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public CategoryController(MongoDbService mongoDbService, TokenService tokenService, IHubContext<NotificationHub> hubContext)
        {
            _categories = mongoDbService.Database?.GetCollection<Category>("categoryCol");
            _files = mongoDbService.Database?.GetCollection<FileModel>("files");
            _tokenService = tokenService;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categories.Find(FilterDefinition<Category>.Empty).ToListAsync();
        }
        [Authorize(Roles = "Player")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetbyId(string id)
        {
            var filter = Builders<Category>.Filter.Eq(i => i.CategoryId, id);
            var categoryr = _categories.Find(filter).FirstOrDefault();
            return categoryr is null ? NotFound() : Ok(categoryr);

        }
        [HttpPost("Add")]
        public async Task<ActionResult> AddCategory(Category category)
        {
            category.CategoryId = null;
            await _categories.InsertOneAsync(category);
            //var token = _tokenService.CreateToken(category);

            //return Ok(new { Token = token });

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "New record inserted, this is from SignalR!");
            return CreatedAtAction(nameof(GetbyId), new { id = category.CategoryId }, category);
        }
        [HttpPost("upload")]
        public async Task<ActionResult> AddFile(FileModel file)
        {
            file.Id = null;
            await _files.InsertOneAsync(file);
            //var token = _tokenService.CreateToken(category);

            //return Ok(new { Token = token });
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "New File inserted, this is from SignalR!");
            return Ok();
        }
        [HttpPost("uploadfile")]
        public async Task<ActionResult> AddFilea(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var fileModel = new FileModel
            {
                FileName = file.FileName,
                FileType = file.ContentType,
                FileContent = stream.ToArray()
            };

            //file.Id = null;
            await _files.InsertOneAsync(fileModel);

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "New File inserted, this is from SignalR!");
            //var token = _tokenService.CreateToken(category);

            //return Ok(new { Token = token });
            return Ok();
        }

        [HttpGet("download/{fileId}")]
        public ActionResult GetFileById(string fileId) {

            var filter = Builders<FileModel>.Filter.Eq(i => i.Id, fileId);

            var file = _files.Find(filter).FirstOrDefault();

            if (file == null) {
                return NotFound();
            }

            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = file.FileName,
                Inline = false
            };

            Response.Headers.Append("Content-Disposition", contentDisposition.ToString());

            return File(file.FileContent, file.FileType);
        }

        [HttpPut]
        public async Task<ActionResult> Updatecategory(Category category)
        {
            var filter = Builders<Category>.Filter.Eq(i => i.CategoryId, category.CategoryId);
            //var theupdate = Builders<category>.Update
            //    .Set(x => x.FirstName, category.FirstName)
            //    .Set(x => x.LastName, category.LastName)
            //    .Set(x => x.Email, category.Email);
            //await _categories.UpdateOneAsync(filter, theupdate);

            await _categories.ReplaceOneAsync(filter, category);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletecategory(string id)
        {
            var filter = Builders<Category>.Filter.Eq(i => i.CategoryId, id);
            await _categories.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
