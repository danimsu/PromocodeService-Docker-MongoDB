using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromocodeService.Models
{
    public interface IPromocodeRepository
    {
        // api/[GET]
        Task<IEnumerable<Promocode>> GetAllPromocodes(long id);

        // api/1/[GET]
        Task<Promocode> GetPromocode(string code);

        // api/[POST]
        Task Create(Promocode promocode);

        // api/[PUT]
        Task<bool> ApplyPromocode(Promocode promocode);

        Task<long> GetNextId();
    }
}
