using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02_New.Models
{
	[Table("Categories")]
	public class Categories
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryID { get; set; }
		[Required]
		public string CategoryName { get; set; }
		public string? Description { get; set; }
        public ICollection<Products>? Products { get; set; }
    }
}

