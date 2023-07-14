using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment02_New.Validation
{
	public class RequireDateValidation :ValidationAttribute
	{
		public RequireDateValidation() 
		{
			ErrorMessage = "Your required date must be greater than now";
		}
        public override bool IsValid(object? value)
        {
            if(value == null) {
				return false;
			}
			DateTime dateTime = DateTime.Parse(value.ToString());
			return dateTime > DateTime.Now;
        }
    }
}

