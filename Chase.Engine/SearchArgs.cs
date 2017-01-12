namespace Chase.Engine
{
    public class SearchArgs
    {
        public int MaxDepth { get; set; }

        public int MaxSeconds { get; set; }

        public bool EnableCaching { get; set; }

        public SearchArgs(int maxDepth, int maxSeconds)
            : this(maxDepth, maxSeconds, true)
        {
        }

        public SearchArgs(int maxDepth, int maxSeconds, bool enableCaching)
        {
            MaxDepth = maxDepth;
            MaxSeconds = maxSeconds;
            EnableCaching = enableCaching;
        }
    }
}