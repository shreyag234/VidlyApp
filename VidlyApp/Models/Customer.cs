using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //Using this to override data conventions and to change the default data type 

namespace VidlyApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Please enter the customer name.")]          //consider it as a primary key
        [StringLength(255)]//makes the data type length to 255 characters 
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType  MembershipType { get; set; } //navigation property
        //navigates to navigate from one type to another
        //connects customer and membership type 
        [Display (Name ="Membership Type")]
        public byte MembershipTypeId { get; set; } //acts as a foreign key 
        //recognized by the entity framework 
        [Min18YearsIfMember]
        public DateTime? BirthDate { get; set; }
        
    }
}