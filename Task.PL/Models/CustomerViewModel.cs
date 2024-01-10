// CustomerViewModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TaskApp.PL.Models
{

    public class CustomerViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Code is required.")]
        [MinLength(3, ErrorMessage = "MinLength Is 3 Charachters")]
        public string Code { get; set; }


        [Display(Name = "Date Of Registration")]
        [Required(ErrorMessage = "Date of Registration is required.")]
        [DataType(DataType.Date)]

        public DateTime DateOfRegistration { get; set; }


        [Display(Name = "Customer Name")]

        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }



        // Add foreign key to relate products to customers
        [Required]
        public List<ProductViewModel> Products { get; set; }
    }

}
