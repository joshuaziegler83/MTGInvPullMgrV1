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

        [HttpPost]
        [Route("api/Customer/")]
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
        [Route("api/Customer/")]
        public IHttpActionResult GetAllCustomers()
        {
            var service = CustomerService();
            var customerList = service.GetAllCustomers();
            return Ok(customerList);
        }

        [HttpGet]
        [Route("api/Customer/email={email}")]
        public IHttpActionResult GetCustomersByEmail(string email)
        {
            string[] emailSplit = email.Split('.');
            string fmtEmail = emailSplit[0];//remove the .suffix between customer facing form and the request
            CustomerServices customerServices= CustomerService();
            var customer = customerServices.GetCustomerByEmail(fmtEmail);
            return Ok(customer);
        }

        [HttpPut]
        [Route("api/Customer/")]
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

