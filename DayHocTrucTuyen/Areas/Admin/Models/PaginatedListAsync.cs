using Microsoft.EntityFrameworkCore;

//Phân trang sử dụng IQueryable of database
namespace DayHocTrucTuyen.Areas.Admin.Models
{
    public class PaginatedListAsync<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int Pagesize { get; private set; }

        public int TotalRecords { get; private set; }

        public PaginatedListAsync(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalRecords = count;
            Pagesize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedListAsync<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedListAsync<T>(items, count, pageIndex, pageSize);
        }
    }
}
