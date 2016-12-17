namespace Chase.Engine
{
    public class SearchArgs
    {
        public int MaxDepth { get; set; }

        public int MaxSeconds { get; set; }

        public SearchArgs(int maxDepth, int maxSeconds)
        {
            MaxDepth = maxDepth;
            MaxSeconds = maxSeconds;
        }
    }
}