using System.Linq;
using System.Text;
 
using Microsoft.EntityFrameworkCore;
using TaskApp.BLL.Interfaces;
using TaskApp.DAL.Contexts;
using TaskApp.DAL.Entities;

namespace TaskApp.BLL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MvcAppDbContext context) : base(context)
        {
        }

        // Implement additional methods specific to the ProductRepository...
    }
}
