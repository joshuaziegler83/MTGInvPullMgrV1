﻿using MTGInvPullMgr.Data;
using MTGInvPullMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Services
{
    public class CustomerServices
    {
        public CustomerDetail GetCustomerByEmail(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.Email == email);
                return
                    new CustomerDetail
                    {
                        Email = entity.Email,
                        NameFirst = entity.NameFirst,
                        NameLast = entity.NameLast
                    };
            }

        }

        public IEnumerable<CustomerDetail> GetAllCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Customers
                        .Select(
                            e =>
                                new CustomerDetail
                                {
                                    Email = e.Email,
                                    NameFirst = e.NameFirst,
                                    NameLast = e.NameLast
                                }
                                );
                return query.ToArray();
            }
        }

        public bool CreateNewCustomer(CustomerCreate model)
        {
            var entity =
                new Customer()
                {
                    Email = model.Email,
                    NameFirst = model.NameFirst,
                    NameLast = model.NameLast
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateCustomer(CustomerEdit model)
        {
            using (var ctx = new ApplicationDbContext()) 
            {
                var entity =
                   ctx
                       .Customers
                       .Single(e => e.Email == model.Email);
                entity.Email = model.Email;
                entity.NameFirst = model.NameFirst;
                entity.NameLast = model.NameLast;
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
