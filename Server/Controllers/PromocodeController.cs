using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromocodeService.Models;

namespace PromocodeService.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class PromocodeController : Controller
    {
        private readonly IPromocodeRepository _repo;

        public PromocodeController(IPromocodeRepository repo)
        {
            _repo = repo;
        }

        // GET api/promocodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promocode>>> Get(long id)
        {
            return new ObjectResult(await _repo.GetAllPromocodes(id));
        }

        //GET: api/promocodes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Promocode>> Get(string code)
        {
            var promocode = await _repo.GetPromocode(code);
            
            if (promocode == null)
                return new NotFoundResult();

            return new ObjectResult(promocode);
        }

        // POST api/promocodes
        [HttpPost]
        public async Task<ActionResult<Promocode>> Post([FromBody] Promocode promocode)
        {
            promocode.Id = await _repo.GetNextId();
            promocode.Code = CodeGenerator.Get(promocode.Id);
            await _repo.Create(promocode);
            return new OkObjectResult(promocode);
        }

        // PUT api/promocodes
        [HttpPut("{code}")]
        public async Task<IActionResult> Set(string code)
        {
            var promocode = await _repo.GetPromocode(code);

            if (promocode == null)
                return new NotFoundResult();

            await _repo.ApplyPromocode(promocode);
            return new OkResult();
        }
    }
}