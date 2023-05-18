using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace VidlyApp.ViewModel
{
    public class MovieFormViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number in stock")]
        [Range(1, 12, ErrorMessage = "This field number must be between 1 to 12")]
        public byte? NumebrInStock { get; set; }

        [Required(ErrorMessage = "The Genre filed in required")]
        [Display(Name = "Select Genre")]
        public byte? GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie"; //making the code simple
                //not using if-else 
            }
        }

        //default constuctor that is used to create movies
        public MovieFormViewModel()
        {
            Id = 0;
            //making sure the hidden Id field in the form is populated 
        }
        //creating a constructor that takes a movie object
        public MovieFormViewModel(Movies movies)
        {
            Id = movies.Id;
            Name = movies.Name;
            ReleaseDate = movies.ReleaseDate;
            NumebrInStock = movies.NumebrInStock;
            GenreId = movies.GenreId;
        }
    }

    
}