// CustomerDataController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TaskApp.DAL.Contexts;
using TaskApp.DAL.Entities;
using TaskApp.PL.Models;
using TaskApp.PL.Reports;

[Authorize]
public class CustomerDataController : Controller
{
    private readonly MvcAppDbContext _context;

    public CustomerDataController(MvcAppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int id)
    {
        var customerData = _context.Customers
            .Include(c => c.Products)
            .Where(c => c.Id == id)
            .FirstOrDefault();

        var viewModel = new CustomerViewModel
        {
            Id = customerData.Id,
            Code = customerData.Code,
            DateOfRegistration = customerData.DateOfRegistration,
            CustomerName = customerData.CustomerName,
            Products = customerData.Products.Select(p => new ProductViewModel
            {
                Name = p.Name,
                Price = p.Price
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult AddCustomerData()
    {
        var viewModel = new AddCustomerDataViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AddCustomerData(AddCustomerDataViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Products == null)
            {
                model.Products = new List<ProductViewModel>();
            }

            var customer = new Customer
            {
                Code = model.Code,
                DateOfRegistration = model.DateOfRegistration,
                CustomerName = model.CustomerName,
                Products = model.Products
                    .Where(p => !string.IsNullOrWhiteSpace(p.Name) || p.Price.HasValue)
                    .Select(p => new Product
                    {
                        Name = p.Name,
                        Price = p.Price ?? 0 // Use 0 as default if Price is null
                }).ToList()
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("GetCustomersReport", new { id = customer.Id });
        }

        return View(model);
    }



    public IActionResult GetCustomersReport()
    {
        CustomerReport customerReport = new CustomerReport();
        return View(customerReport);
    }
}
