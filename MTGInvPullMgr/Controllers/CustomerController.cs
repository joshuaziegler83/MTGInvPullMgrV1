using Microsoft.AspNet.Identity;
using MTGInvPullMgr.Models;
using MTGInvPullMgr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MTGInvPullMgr.Controllers
{

    [Authorize]
    public class CustomerController : ApiController
    {
        private CustomerServices CustomerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var CustomerServices = new CustomerServices(userId);
            return CustomerServices;
        }

        public IHttpActionResult Post(CustomerCreate customerCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CustomerService();
            if (!service.CreateNewCustomer(customerCreate))
                return InternalServerError();
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAllCustomers()
        {
            var service = CustomerService();
            var customerList = service.GetAllCustomers();
            return Ok(customerList);
        }

        [HttpGet]
        public IHttpActionResult GetCustomersByEmail(string email)
        {
            CustomerServices customerServices= CustomerService();
            var customer = customerServices.GetCustomerByEmail(email);
            return Ok(customer);
        }

        public IHttpActionResult Put(CustomerEdit customerEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CustomerService();
            if (!service.UpdateCustomer(customerEdit))
                return InternalServerError();
            return Ok();
        }
    }
}

