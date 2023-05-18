using System.ComponentModel.DataAnnotations;

namespace VidlyApp.Models
{
    public class Genre
    {
        public byte Id { get; set; }
       
        [StringLength(255)]
        public string Name { get; set; }

    }
}