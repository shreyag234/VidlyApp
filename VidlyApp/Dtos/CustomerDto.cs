using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace VidlyApp.Dtos
{
    public class CustomerDto
    {
        //copying all properties of thr customer model
        //ijmport namespaces 
        public int Id { get; set; }
              //consider it as a primary key
        [StringLength(255)]//makes the data type length to 255 characters 
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
        
        public byte MembershipTypeId { get; set; } //acts as a foreign key 
        //recognized by the entity framework 
        //[Min18YearsIfMember]
        public DateTime? BirthDate { get; set; }

        //to access the membership name in datatables 
      
        public MembershipTypeDto MembershipType { get; set; }

    }
}