using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
    public class RestaurantWriteModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}
