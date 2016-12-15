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

        public long ElapsedMilliseconds { get; set; }

        public int TotalMoves { get; set; }

        public int CurrentMove { get; set; }

        public decimal NodesPerSecond { get { return SearchedNodes / (ElapsedMilliseconds / 1000.0M); } }
    }
}
