using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02_New.Models
{
	[Table("OrderDetails")]
	public class OrderDetails
	{

		public int OrderID { get; set; }
		[ForeignKey("OrderID")]
		public Orders Orders { get; set; }
		public int ProductID { get; set; }
		[ForeignKey("ProductID")]
		public Products Products { get; set; }
		[Column(TypeName ="money")]
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
	}
}

