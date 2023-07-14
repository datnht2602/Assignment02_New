using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02_New.Models
{
	[Table("Suppliers")]
	public class Suppliers
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SupplierID { get; set; }
		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string? Address { get; set; }
        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "The phone number is not valid")]
        public string Phone { get; set; }
		public ICollection<Products>? Products { get; set; }
	}
}

