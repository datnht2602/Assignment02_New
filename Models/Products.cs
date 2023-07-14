using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02_New.Models
{
	[Table("Products")]
	public class Products
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductID { get; set; }
		[Required]
		[Display(Name ="Name")]
		[StringLength(20,MinimumLength =3,ErrorMessage ="ProductName is required")]
		public string ProductName { get; set; }
		[Required]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
		[ForeignKey("SupplierID")]
		public Suppliers? Suppliers { get; set; }
		[Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
		[ForeignKey("CategoryID")]
		public Categories? Categories { get; set; }
		[Required]
		[Display(Name ="Quantity")]
		public int QuantityPerUnit { get; set; }
		[Required]
		[Column(TypeName ="Money")]
		[Display(Name ="Price")]
		public decimal UnitPrice { get; set; }
	
		public string? ProductImage { get; set; }
	}
}

