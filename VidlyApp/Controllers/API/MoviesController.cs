using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Net;
using System.Net.Http;
using VidlyApp.Models;
using VidlyApp.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace VidlyApp.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/movie
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumebrInStock > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery
                .ToList()
               .Select(Mapper.Map<Movies, MovieDto>);
            //return _context.Movies.ToList().Select(Mapper.Map<Movies, MovieDto>);
        }

        //GET api/movie/1
        public IHttpActionResult GetMovies(int Id)
        {
            var movies = _context.Movies.SingleOrDefault(m => m.Id == Id);
            if (movies == null)
                return BadRequest();
            return Ok(Mapper.Map<Movies, MovieDto>(movies));
        }

        //POST api/movie/
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movies = Mapper.Map<MovieDto, Movies>(movieDto);
            _context.Movies.Add(movies);
            _context.SaveChanges();
            //adding the id that the client receives 
            movieDto.Id = movies.Id;
            return Created(new Uri(Request.RequestUri + "/" + movies.Id), movieDto);
        }

        //PUT api/movie/1
        [HttpPut]
        public void UpdateMovies(int id, MovieDto movieDto)
        {
            //checking model state 
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id); //getting data from the db context 
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map<MovieDto, Movies>(movieDto, movieInDb);
            _context.SaveChanges();

        }

        //DELETE api/movie/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            

        }
    }
}