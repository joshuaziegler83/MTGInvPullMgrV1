using MTGInvPullMgr.Data;
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
        private readonly Guid _customerId;
        public CustomerServices(Guid customerId) 
        {
            _customerId = customerId;
        }

        public IEnumerable<Customer> GetCustomerByEmail(string email)
        {
            /*char[] ch = {'.'};
            string fmtEmail = email.TrimStart(ch);*/
            
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Customers
                        .Where(e => e.Email.Contains(email));
                return query.ToArray();
                
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
