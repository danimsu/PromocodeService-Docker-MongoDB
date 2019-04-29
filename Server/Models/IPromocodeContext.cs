using MongoDB.Driver;

namespace PromocodeService.Models
{
    public interface IPromocodeContext
    {
        IMongoCollection<Promocode> Promocodes { get; }
    }
}
