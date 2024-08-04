using MongoDB.Driver;
using System.IO;
using testProjectApis.Models;

namespace testProjectApis.services
{
    public class FileService
    {
        private readonly IMongoCollection<FileModel> _files;

        public FileService(IMongoDatabase database)
        {
            _files = database.GetCollection<FileModel>("Files");
        }

        public async Task<List<FileModel>> GetAsync() =>
            await _files.Find(file => true).ToListAsync();

        public async Task<FileModel> GetAsync(string id) =>
            await _files.Find<FileModel>(file => file.Id == id).FirstOrDefaultAsync();

        public async Task<FileModel> CreateAsync(FileModel file)
        {
            await _files.InsertOneAsync(file);
            return file;
        }
    }
}
