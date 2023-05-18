using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net;
using System.Net.Http;
using VidlyApp.Models;
using VidlyApp.Dtos;
using AutoMapper;
using System.Web.Http;

namespace VidlyApp.Controllers.API
{
    public class CustomersController : ApiController
    {

        private ApplicationDbContext _context;
        //initialize the DBcontext in the controller
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/customers   : this is the built in convention in the web API 
        public IHttpActionResult GetCustomers() //an action to get the list of customers 
        {
            var customerDto = _context.Customers.
                Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

                return Ok(customerDto);

            //using a linq select method                         //passing the delegate that does the mapping 

        }

        //to get a single customer
        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null) //this method takes an enumeration and specifies the type of error
                return BadRequest();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));

        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer (CustomerDto customerdto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //creating a customer object using mapping 
            var customer = Mapper.Map<CustomerDto, Customer>(customerdto); //make note of what the sender and recievers are 
            _context.Customers.Add(customer);
            _context.SaveChanges();

            //getting and returning the id to client 
            customerdto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerdto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            //validating the cutomer
            if (!ModelState.IsValid)
                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (CustomerInDb == null)
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            //updating all the details of the customer
            Mapper.Map<CustomerDto, Customer>(customerDto, CustomerInDb);
            //CustomerInDb.Name = customer.Name;
            //CustomerInDb.BirthDate = customer.BirthDate;
            //CustomerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            //CustomerInDb.MembershipTypeId = customer.MembershipTypeId;
            /** you can use a tool like AutoMapper to map out all the
             * customer details and update them without specifying all the 
             * properties here
             */
            _context.SaveChanges();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            //removing the customer form the memory 
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }

}
