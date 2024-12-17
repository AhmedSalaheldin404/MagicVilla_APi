using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaApiController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            
        }
        //gett all villas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            IEnumerable<Villa> villalist = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villalist) );
        }
        //GET ONE VILLA BY ID
        [HttpGet("{id:int}", Name = "Getvilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult<VillaDto>> GetAVilla(int id)
        {

            if (id == 0) { return BadRequest(); }

            var villa =  await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaDto Createdto)
        {
            if (Createdto == null) { return BadRequest(Createdto); }
            if (Createdto.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

            Villa model=_mapper.Map<Villa>(Createdto);

            //Villa model = new Villa() { Amenity= Createdto.Amenity,
            //Id= Createdto.Id,
            //Description= Createdto.Description,
            //CreatedDate= Createdto.CreatedDate,
            //Name= Createdto.Name,
            //Rate= Createdto.Rate,
            //ImageUrl= Createdto.ImageUrl,
            //Occupancy= Createdto.Occupancy
            //};

          await  _db.Villas.AddAsync(model);
           await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new { id = Createdto.Id }, Createdto);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAVilla(int id) {
            if (id == 0) { return BadRequest(); }

            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
           _db.Remove(villa);
          await  _db.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaDto updatedto)
        {
            if (updatedto == null || id != updatedto.Id) { return BadRequest(); }
            //var vilaa=_db.Villas.FirstOrDefault(v => v.Id == id);
            //vilaa.Name =villa.Name;
            //vilaa.Description =villa.Description;


            Villa model = _mapper.Map<Villa>(updatedto);
            //Villa model = new Villa()
            //{
            //    Amenity = villa.Amenity,
            //    Id = villa.Id,
            //    Description = villa.Description,
            //    CreatedDate = villa.CreatedDate,
            //    Name = villa.Name,
            //    Rate = villa.Rate,
            //    ImageUrl = villa.ImageUrl,
            //    Occupancy = villa.Occupancy
            //}; 
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "UpdateAVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateAVilla(int id, JsonPatchDocument<VillaDto>patchdto)
        {
            if(patchdto==null ||id==0) { return BadRequest(); }
            var villa=await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            VillaDto villadto=_mapper.Map<VillaDto>(patchdto);
            //VillaDto villadto = new()
            //{
            //    Amenity = villa.Amenity,
            //    Id = villa.Id,
            //    Description = villa.Description,
            //    CreatedDate = villa.CreatedDate,
            //    Name = villa.Name,
            //    Rate = villa.Rate,
            //    ImageUrl = villa.ImageUrl,
            //    Occupancy = villa.Occupancy
            //};


            if (villa==null) { return BadRequest(); }
            patchdto.ApplyTo(villadto, ModelState);
            Villa model = _mapper.Map<Villa>(patchdto);
            //Villa model = new()
            //{
            //    Amenity = villadto.Amenity,
            //    Id = villadto.Id,
            //    Description = villadto.Description,
            //    CreatedDate = villadto.CreatedDate,
            //    Name = villadto.Name,
            //    Rate = villadto.Rate,
            //    ImageUrl = villadto.ImageUrl,
            //    Occupancy = villadto.Occupancy
            //};
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            _db.Villas.Update(model);
           await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
 