// ProductViewModel.cs
using System.ComponentModel.DataAnnotations;
namespace TaskApp.PL.Models
{

    public class ProductViewModel
    {

        [Required(ErrorMessage = "Product Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }

    }

}
