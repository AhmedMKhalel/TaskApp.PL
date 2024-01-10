using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace TaskApp.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }

        public ICustomerRepository CustomerRepository { get; set; }
    }
}
