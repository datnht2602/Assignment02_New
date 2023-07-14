using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment02_New.Validation;

namespace Assignment02_New.Models
{
	[Table("Orders")]
	public class Orders
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderID { get; set; }
		public int CustomerID { get; set; }
		[ForeignKey("CustomerID")]
		public Customer Customer { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.Now;
		[RequireDateValidation]
		public DateTime RequiredDate { get; set; }
		public DateTime ShippedDate { get; set; }
		[Required]
		public bool Freight { get; set; }
		[Required]
		public string ShipAddress { get; set; }
		public ICollection<OrderDetails> OrderDetails { get; set; }
	}
}

