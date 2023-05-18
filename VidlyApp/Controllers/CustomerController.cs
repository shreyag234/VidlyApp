using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyApp.Models;
using VidlyApp.ViewModel;
using System.Data.Entity;

namespace VidlyApp.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        // --------------- SECTION 2 EXCERCISE ---------------
        //creating an ienumerable customer list 
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };
        //}

        //************************ QUERYING OBJECTS - CODE FIRST ***************
        private ApplicationDbContext _context; //create app DBcontext 
        //initialize it in the constructor of the 
        public CustomerController()
        {
            _context = new ApplicationDbContext(); //this is a proper disposable object
        }
        //dispose this object by overriding the dispose method of the base controller class
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //creating an action to create new customers 
        //use HttpPost when dealing with modifying data **never use HttpGet**!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) //model binding
            //MVC framework binds the model to the request data
        {
            //validating 
            if (!ModelState.IsValid)
            {
                var viewmodel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewmodel);
            }
            if(customer.Id == 0)
                _context.Customers.Add(customer); //here it takes all the data during run time but does not presist 
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                //to update:
                /** you can use the TryUpdateModel() method or use auto-mapper 
                 * like Mapper.Map(customer, customerInDb) to update all values in 
                 * the customer table. However, you can have many security holes while doing so
                 */
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
               
            }
            //presists data when the below method is called 
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var ViewModel = new CustomerFormViewModel{
                Customer = new Customer(), //this creates a new customer so when it checks for validation, it'd set as 0
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", ViewModel);
        }
        public ActionResult Index()
        {
            //this property is the DBset defined in the DB context 
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();        //toList() immediately executes the query 
            //this only executes when the customers object is itterated 

            return View(//customers
                        );
        }
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
           // var customer = _context.Customers.Include(c => c.MembershipType).ToList();

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        public ActionResult Edit(int Id) 
        {
            var cusotmer = _context.Customers.SingleOrDefault(c => c.Id == Id);
            if (cusotmer == null)
               return HttpNotFound();
            var Viewmodel = new CustomerFormViewModel
            {
                Customer = cusotmer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", Viewmodel);
        }

    }

}