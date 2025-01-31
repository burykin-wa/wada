using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Pagination
{
    public class Page<T>
    {
        public T[]? Content { get; set; }
        public bool Empty { get; set; }
        public bool First { get; set; }
        public bool Last { get; set; }
        public int Number { get; set; }
        public int NumberOfElements { get; set; }
        public PageableObject Pageable { get; set; }
        public int Size { get; set; }
        public SortObject[]? Sort { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
    }
}
