using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
