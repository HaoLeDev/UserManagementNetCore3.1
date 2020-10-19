using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace UserManagerNetCore.Infrastructure.Filters
{
    public class Response<T>
    {
        public Response()
        {

        }
            
        public Response(IEnumerable<T> data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public int StatusCode { get { return Microsoft.AspNetCore.Http.StatusCodes.Status200OK; } }
        public IEnumerable<T> Data { get; set; }
    }
}
