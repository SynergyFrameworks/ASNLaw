using System;

namespace Infrastructure
{
    public class BaseListPageInfo
    {
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }

        public int TotalPageCount
        {
            get { return (int)(PageSize == 0 ? 0 : Math.Ceiling(((double)TotalItemCount / PageSize))); }
        }

        public int PageSize { get; set; }
        public string SearchQuery { get; set; }
        public string Sort { get; set; }
        public bool SortAsc { get; set; }
    }
}
