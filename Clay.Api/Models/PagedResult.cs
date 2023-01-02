using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class PagedResult<T>
    {
        private const int DefaultPage = 1;

        private readonly IEnumerable<T> _items;
        private readonly long _totalItemCount;
        private readonly int _page;
        private readonly int _pageSize;
        private readonly int _pageCount;

        public PagedResult(IEnumerable<T> items, long totalItemCount, int? page, int? pageSize)
        {
            if (pageSize == null)
                _pageSize = 50;
            else
                _pageSize = pageSize.GetValueOrDefault();

            _items = items;
            _totalItemCount = totalItemCount;

            _pageCount = (int)Math.Ceiling((double)TotalItemCount / PageSize);

            if (page == null || page < DefaultPage)
            {
                page = DefaultPage;
            }

            if (page > _pageCount)
            {
                page = _pageCount;
            }

            _page = page.GetValueOrDefault();
        }


        public IEnumerable<T> Items
        {
            get
            {
                return _items;
            }
        }

        public long TotalItemCount
        {
            get
            {
                return _totalItemCount;
            }
        }

        public int Page
        {
            get
            {
                return _page;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return Page > DefaultPage;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return Page < PageCount;
            }
        }
    }
}
