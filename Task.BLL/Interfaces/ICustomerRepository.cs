using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.DAL.Entities;

namespace TaskApp.BLL.Interfaces
{
    public interface ICustomerRepository : IGenericRepoository<Customer>
    {
        Task<string> GetProductsByCustomerId(int? id);
    }
}
