using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine.Interfaces
{
    interface ISearchAlgorithm
    {
        SearchResult GetBestMove(Position position, int searchDepth);

        event EventHandler<SearchStatus> OnNewResult;
    }
}
