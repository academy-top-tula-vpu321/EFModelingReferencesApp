using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModelingReferencesApp
{
    public class Company
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        //public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }

        public virtual List<Employee>? Employees { get; set; }
    }
}
