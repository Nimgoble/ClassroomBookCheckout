using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassroomBookCheckout.Models
{
    public class Student
    {
        [Key]
        public Int32 ID_Student { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
