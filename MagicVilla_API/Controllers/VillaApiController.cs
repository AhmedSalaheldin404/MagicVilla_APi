using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        //gett all villas
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            return VillaStore.villalist;
        }

        [HttpGet("{id:int}")]
        public VillaDto GetAVilla(int id)
        {
            return VillaStore.villalist.FirstOrDefault(u=>u.Id==id);
        }
    }
}