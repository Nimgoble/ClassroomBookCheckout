using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClassroomBookCheckout.ViewModels 
{
	public class CheckOutBookViewModel 
	{
		[Required]
		public Int32 StudentID { get; set; }
		[Required]
		public String ISBN { get; set; }
	}
}