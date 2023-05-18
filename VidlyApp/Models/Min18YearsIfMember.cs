using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VidlyApp.Models
{
    public class Min18YearsIfMember: ValidationAttribute //derive this class
    {
        // making a customer validation 
        /**if a member is not 18 years old, the validation error pops up
         * if they want to go for other payment methods
         * than 'Pay as you go'
         */


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance; //gives us access to the containing class 
            if (customer.MembershipTypeId == MembershipType.Unkown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;
            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate os required");
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18) ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years ");
        }
    }
}