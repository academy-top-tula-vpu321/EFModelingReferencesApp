using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModelingReferencesApp
{
    public class Country
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public List<Company>? Companies { get; set; }
    }
}
