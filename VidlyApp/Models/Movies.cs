using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //for overriding the name of the fields 

namespace VidlyApp.Models
{
        //this class is an old POCO object 
        //has the state and behavior of the application
        public class Movies
        {
            public int Id { get; set; }
            [Required]
            public String Name { get; set; }
            [Required]
            [Display(Name ="Release Date")]
            public DateTime ReleaseDate { get; set; }
            
            public DateTime DateAdded { get; set; }
            [Required]
            [Display(Name ="Number in stock")]
            [Range(1,12, ErrorMessage ="This field number must be between 1 to 12")]
            public byte NumebrInStock { get; set; }
            
            public Genre Genre { get; set; }
            [Required (ErrorMessage ="The Genre filed in required")]
            [Display (Name ="Select Genre")]
            
            public byte GenreId { get; set; }
        }

        // /movies/random
    
}