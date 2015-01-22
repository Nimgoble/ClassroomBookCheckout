using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomBookCheckout.ViewModels 
{
	public class CreateBookForCheckOutViewModel 
	{
		public Int32 StudentID { get; set; }
		public String ISBN { get; set; }
		public String Title { get; set; }
	}
}