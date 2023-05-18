using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VidlyApp.Models
{
    public class MembershipType
    {
        //primary key to map out the identity of this entity
       
        public byte Id { get; set; }
        public string MembershipName { get; set; }
        public short SignUpFee { get; set; } //type short because it doesn't need more then 3200 
        public byte DurationInMonths { get; set; } //byte becuase its between 1-12 months
        public byte DiscountRate { get; set; }

        //declaring reference data 
        //can be used in the custom validation 
        public static readonly byte Unkown = 0;
        public static readonly byte PayAsYouGo = 1; 

    }
}