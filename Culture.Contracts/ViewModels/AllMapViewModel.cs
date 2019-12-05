using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
    public class AllMapViewModel
    {
        public IEnumerable<string[]> Dates { get; set; }
        public string Query { get; set; }
        public string Category { get; set; }
    }
}
