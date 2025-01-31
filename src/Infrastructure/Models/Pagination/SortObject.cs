using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Pagination
{
    public class SortObject
    {
        public bool Ascending { get; set; }
        public string? Direction { get; set; }
        public bool IgnoreCase { get; set; }
        public string? NullHandling { get; set; }
        public required string Property { get; set; }
    }
}
