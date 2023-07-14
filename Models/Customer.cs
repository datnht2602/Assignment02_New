using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment02_New.Models
{
	[Table("Customers")]
	public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		
		public int CustomerID { get; set; }
		[Required]
		[StringLength(20,MinimumLength = 6,ErrorMessage ="The length of password is from 6 to 20")]
		public string Password { get; set; }
		[Required]
		[Display(Name ="Username")]
		public string ContactName { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		[Phone(ErrorMessage ="The phone number is not valid")]
		public string Phone { get; set; }
	}
}

