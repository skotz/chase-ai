using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class SearchStatus
    {
        public SearchResult BestMoveSoFar { get; set; }

        public long SearchedNodes { get; set; }

        public long HashLookups { get; set; }

        public long ElapsedMilliseconds { get; set; }

        public int TotalMoves { get; set; }

        public int CurrentMove { get; set; }

        public int Depth { get; set; }

        public string CurrentVariation { get; set; }

        public decimal NodesPerSecond { get { return SearchedNodes / ((ElapsedMilliseconds == 0 ? 1 : ElapsedMilliseconds) / 1000.0M); } }
    }
}
