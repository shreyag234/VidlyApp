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
    public class MovieController : Controller
    {                               //*** mvc4action tab*** for creating an action 
        // GET: Movie
        //------------ SECTION 2/3 - EXCERCISE -----------

        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext(); //making it a proper object
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); //disposing it off 
        }

        //----- SECTION 4 EXCERCISE 
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewmodel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewmodel);
        }
        public ActionResult AddMovie(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);
            if (movie == null)
                HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                
                Genres = _context.Genres.ToList()

            };
            return View("MovieForm", viewModel);

        }
        [HttpPost]
        public ActionResult Save(Movies movies)
        {//validation
            if (!ModelState.IsValid) //checking model state
            {
                var viewmodel = new MovieFormViewModel(movies)
                {
                    Genres = _context.Genres.ToList()

                };
            }
            if (movies.Id == 0)
            {
                movies.DateAdded = DateTime.Now;
                _context.Movies.Add(movies);

            }        
            else
            {
                var MoviesInDb = _context.Movies.Single(m => m.Id == movies.Id);
                MoviesInDb.Name = movies.Name;
                MoviesInDb.ReleaseDate = movies.ReleaseDate;
                MoviesInDb.GenreId = movies.GenreId;
                MoviesInDb.NumebrInStock = movies.NumebrInStock;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }
        public ActionResult Index()
        {
            if (User.IsInRole("CanManageMovies"))
            {
               // var movies = _context.Movies.Include(m => m.Genre).ToList();
                return View();
            }
            else
            {
                return View("ReadOnlyList");
            }
            
        }

        //creating a method to get the list of movies 
        //private object GetMovies()
        //{
        //    return new List<Movies>
        //    {
        //        new Movies{Id = 1, Name = "Shrek"},
        //        new Movies{Id = 2, Name = "Wall-ee"},
        //    };
        //}
        //--------------------------------------------------
        public ActionResult Details(int Id)
        {
            var movies = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            // var customer = _context.Customers.Include(c => c.MembershipType).ToList();

            if(movies == null)
                return HttpNotFound();

            return View(movies);
        }
        public ActionResult Random()
        {
            // creating an instance of the movie model 
            var movie = new Movies() { Name = "Shrek!" };
            /*ViewData["Movies"] = movie;*/ //passing movies using viewdatadictonary 
            //creating a list of customers 
            var customers = new List<Customer>
            {
                new Customer{Name = "Joseph"},
                new Customer{Name = "Michael"}

            };
            //creating a viewmodel object 
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("Id: " + id);
        }
        //an action method that navigates to the movies page
        //the string datatype in C# is a reference type os it is nullable 
        public ActionResult Index2(int? pageIndex, string sortBy) //the '?' makes the value nullable so it's not compulsary 
        {
            //checking and putting a default value for the parameters
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));

        }
        //attribute route
        [Route("movie/release/{year: regex(\\d{4})}/{month: regex(\\d{2}) : range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}