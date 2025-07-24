using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resource
{
    public class SaveCategoryResource
    {
        [Required]
        [MaxLength(30, ErrorMessage = "The maximum length for the name is 50 characters.")]
        public string Name { get; set; }
    }
}
