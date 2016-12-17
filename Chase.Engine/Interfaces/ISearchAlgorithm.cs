using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine.Interfaces
{
    interface ISearchAlgorithm
    {
        SearchResult GetBestMove(Position position, SearchArgs settings);
        
        event EventHandler<SearchStatus> OnNewResult;
    }
}
