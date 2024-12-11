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
        public ActionResult<VillaDto> GetVillas()
        {
            return Ok(VillaStore.villalist);
        }
        //GET ONE VILLA BY ID
        [HttpGet("{id:int}")]
        public ActionResult <VillaDto> GetAVilla(int id)
        {

            if (id == 0) { return BadRequest(); }

            var villa= VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            if (villa == null) { 
                return NotFound();
            }
            return Ok(villa);
        }
    }
}