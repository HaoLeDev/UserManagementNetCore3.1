using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagerNetCore.Infrastructure.Filters
{
    public class PagedResponse<T> : Response<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int Count {
            get
            {
                return (Data != null) ? Data.Count() : 0;
            }
        }

        public PagedResponse(IEnumerable<T> data, int page, int pageSize, int totalRecord)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Message = "success"??"null";
            this.Succeeded = true;
            this.Errors = null;
            this.TotalRecords = totalRecord;
            this.TotalPages = (int)Math.Ceiling((decimal)totalRecord / pageSize);
            this.Data = data;
        }
    }
}
