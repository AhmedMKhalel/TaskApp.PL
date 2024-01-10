using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskApp.DAL.Entities
{
    // Customer.cs
    public class Customer
    {
        public Customer()
        {
            DateOfRegistration = DateTime.Now;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [MinLength(3,ErrorMessage = "MinLength Is 3 Charachters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Date of Registration is required.")]
        public DateTime DateOfRegistration { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }

        // Add foreign key to relate products to customers
        public List<Product> Products { get; set; }
    }
}
