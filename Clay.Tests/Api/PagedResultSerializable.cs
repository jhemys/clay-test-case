namespace Clay.Tests.Api
{
    internal class PagedResultSerializable<T>
    {
        public IEnumerable<T> Items { get; set; }
        public long TotalItemCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
