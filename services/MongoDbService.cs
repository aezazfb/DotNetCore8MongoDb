using MongoDB.Driver;

namespace testProjectApis.services
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectron = _configuration.GetConnectionString("connectDB");
            
            var mongoUrl = MongoUrl.Create(connectron);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database { get { return _database; } }
    }
}
