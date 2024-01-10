using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApp.BLL.Interfaces;
using TaskApp.DAL.Contexts;
using TaskApp.DAL.Entities;

namespace TaskApp.BLL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly MvcAppDbContext _context;

        public CustomerRepository(MvcAppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> GetProductsByCustomerId(int? id)
        {
            if (id == null)
            {
                // Handle the case where id is null (invalid input)
                return "Invalid customer ID";
            }

            // Retrieve customer and associated products using Entity Framework
            var customerWithProducts = await _context.Customers
                .Include(c => c.Products)  // Include related products
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customerWithProducts == null)
            {
                // Handle the case where no customer is found with the given ID
                return "Customer not found";
            }

            // Build a string with product information
            var result = new StringBuilder();
            result.AppendLine($"Customer Name: {customerWithProducts.CustomerName}");

            if (customerWithProducts.Products.Any())
            {
                result.AppendLine("Products:");
                foreach (var product in customerWithProducts.Products)
                {
                    result.AppendLine($"- Product Name: {product.Name}, Price: {product.Price}");
                }
            }
            else
            {
                result.AppendLine("No products associated with this customer.");
            }

            return result.ToString();
        }
    }
}
