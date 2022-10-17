﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Net6.Domain
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        bool Exists(int id);

        Customer GetByEmail(string email);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);
    }
}
