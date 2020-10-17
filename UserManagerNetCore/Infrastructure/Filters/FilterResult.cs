using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagerNetCore.Infrastructure.Filters
{
    public class FilterResult<T>
    {
        public FilterResult(T data, int statusCode, string[]error)
        {
            Data = data;
            this.statusCode = statusCode;
            this.Errors = error;
        }


        public bool Succeeded {
            get {
                if (Errors != null) return false;
                else return true;} 
        }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public int statusCode { get; set; }
        public T Data { get; set; }
    }
}
