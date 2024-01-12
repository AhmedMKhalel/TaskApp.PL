// CustomerDataController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    //private string GetLoggedInUserId()
    //{
    //    // Assuming you are using ASP.NET Core Identity
    //    return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    //}
    public IActionResult Index(int id )
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
            // Map the view model to the entity model
            var customer = new Customer
            {
                Code = model.Code,
                DateOfRegistration = model.DateOfRegistration,
                CustomerName = model.CustomerName,
                // Map other properties as needed
            };

            // Add products to the customer
            customer.Products = model.Products.Select(p => new Product
            {
                Name = p.Name,
                Price = p.Price
            }).ToList();

            // Add the customer to the database
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Redirect to the Index action after successful save
            return RedirectToAction("GetCustomersReport", new { id = customer.Id });
        }

        // Model state is not valid, redisplay the form with validation errors
        return View(model);
    }

    public IActionResult GetCustomersReport()
    {
        CustomerReport customerReport = new CustomerReport(); 
        return View(customerReport);
    }
}
