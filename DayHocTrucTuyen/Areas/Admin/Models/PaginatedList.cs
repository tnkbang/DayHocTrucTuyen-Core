using Microsoft.EntityFrameworkCore;

//Phân trang sử dụng danh sách bình thường với offset và limit page
namespace DayHocTrucTuyen.Areas.Admin.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageOffset { get; private set; }
        public int TotalPages { get; private set; }
        public int Pagesize { get; private set; }

        public int TotalRecords { get; private set; }

        public PaginatedList(List<T> items, int count, int pageOffset, int pageSize)
        {
            PageOffset = pageOffset;
            TotalRecords = count;
            Pagesize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public static PaginatedList<T> Create(List<T> source, int pageOffset, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip(pageOffset).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageOffset, pageSize);
        }
    }
}
