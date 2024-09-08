using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFModelingReferencesApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        //[Required]
        public int? CompanyId { get; set; }

        //public string? CompanyTitle { get; set; }

        //[ForeignKey("CommandId")]
        public virtual Company? Company { get; set; } = null!;

        public virtual Position? Position {  get; set; } 
    }
}
