// CustomerReportModel.cs
using System;
using System.Collections.Generic;

namespace TaskApp.PL.Models
{
    public class CustomerReportModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string CustomerName { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
