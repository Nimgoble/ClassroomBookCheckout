using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassroomBookCheckout.Models
{
    public class Book
    {
        [Key]
        public Int32 ID_Book { get; set; }
        [Required]
        public String ISBN { get; set; }
        [Required]
        public String Title { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
