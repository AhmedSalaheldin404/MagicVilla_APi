using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaApiController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        //gett all villas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDto> GetVillas()
        {
            return Ok(_db.Villas.ToList());
        }
        //GET ONE VILLA BY ID
        [HttpGet("{id:int}", Name = "Getvilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetAVilla(int id)
        {

            if (id == 0) { return BadRequest(); }

            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villadto)
        {
            if (villadto == null) { return BadRequest(villadto); }
            if (villadto.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

            Villa model = new Villa() { Amenity=villadto.Amenity,
            Id=villadto.Id,
            Description=villadto.Description,
            CreatedDate=villadto.CreatedDate,
            Name=villadto.Name,
            Rate=villadto.Rate,
            ImageUrl=villadto.ImageUrl,
            Occupancy=villadto.Occupancy
            };

            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villadto.Id }, villadto);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAVilla(int id) {
            if (id == 0) { return BadRequest(); }

            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
           _db.Remove(villa);
            _db.SaveChanges();
            return NoContent();

        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villa)
        {
            if (villa == null || id != villa.Id) { return BadRequest(); }
            //var vilaa=_db.Villas.FirstOrDefault(v => v.Id == id);
            //vilaa.Name =villa.Name;
            //vilaa.Description =villa.Description;
            Villa model = new Villa()
            {
                Amenity = villa.Amenity,
                Id = villa.Id,
                Description = villa.Description,
                CreatedDate = villa.CreatedDate,
                Name = villa.Name,
                Rate = villa.Rate,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy
            }; 
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdateAVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateAVilla(int id, JsonPatchDocument<VillaDto>patchdto)
        {
            if(patchdto==null ||id==0) { return BadRequest(); }
            var villa=_db.Villas.FirstOrDefault(u=>u.Id == id);
            VillaDto villadto = new()
            {
                Amenity = villa.Amenity,
                Id = villa.Id,
                Description = villa.Description,
                CreatedDate = villa.CreatedDate,
                Name = villa.Name,
                Rate = villa.Rate,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy
            };


            if (villa==null) { return BadRequest(); }
            patchdto.ApplyTo(villadto, ModelState);
            Villa model = new()
            {
                Amenity = villadto.Amenity,
                Id = villadto.Id,
                Description = villadto.Description,
                CreatedDate = villadto.CreatedDate,
                Name = villadto.Name,
                Rate = villadto.Rate,
                ImageUrl = villadto.ImageUrl,
                Occupancy = villadto.Occupancy
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
 