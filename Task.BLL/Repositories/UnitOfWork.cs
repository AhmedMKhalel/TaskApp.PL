using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using TaskApp.BLL.Interfaces;

namespace TaskApp.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            CustomerRepository = customerRepository;
            ProductRepository = productRepository;

        }
        public ICustomerRepository CustomerRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }

    }
}
