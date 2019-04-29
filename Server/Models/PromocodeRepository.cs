using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PromocodeService.Models
{
    public class PromocodeRepository: IPromocodeRepository
    {
        private readonly IPromocodeContext _context;

        public PromocodeRepository(IPromocodeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of promocodes by the specified user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Promocode>> GetAllPromocodes(long id)
        {
            FilterDefinition<Promocode> filter = Builders<Promocode>.Filter.Eq(p => p.UserId, id);
            return await _context
                .Promocodes
                .Find(filter)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a promocode by the specified code
        /// </summary>
        /// <param name="code">The promocode unique text</param>
        /// <returns></returns>
        public Task<Promocode> GetPromocode(string code)
        {
            FilterDefinition<Promocode> filter = Builders<Promocode>.Filter.Eq(p => p.Code, code);
            return _context
                .Promocodes
                .Find(filter)
                .FirstOrDefaultAsync();
        }
        
        /// <summary>
        /// Makes the promocode no longer valid when user applies it
        /// </summary>
        /// <param name="promocode"></param>
        /// <returns></returns>
        public async Task<bool> ApplyPromocode(Promocode promocode)
        {
            promocode.IsActive = false;
            ReplaceOneResult replaceResult =
                await _context
                .Promocodes
                .ReplaceOneAsync(
                    filter: p => p.Id == promocode.Id,
                    replacement: promocode);
            return replaceResult.IsAcknowledged && replaceResult.ModifiedCount > 0;
        }

        public async Task Create(Promocode promocode)
        {
            await _context.Promocodes.InsertOneAsync(promocode);
        }

        public async Task<long> GetNextId()
        {
            return await _context.Promocodes.CountDocumentsAsync(new BsonDocument()) + 1;
        }

    }
}
