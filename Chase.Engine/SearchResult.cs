using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class SearchResult
    {
        public int Score { get; set; }

        public long Evaluations { get; set; }

        public long HashLookups { get; set; }

        public string PrimaryVariation { get; set; }

        public Move BestMove { get; set; }
    }
}
