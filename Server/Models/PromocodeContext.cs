using MongoDB.Driver;
using PromocodeService.Config;

namespace PromocodeService.Models
{
    public class PromocodeContext: IPromocodeContext
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Sets up database
        /// </summary>
        /// <param name="config"></param>
        public PromocodeContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            db = client.GetDatabase(config.Database);
        }

        /// <summary>
        /// Gets a collection of promocodes
        /// </summary>
        public IMongoCollection<Promocode> Promocodes => db.GetCollection<Promocode>("Promocodes");
    }
}
