using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagerNetCore.Infrastructure.Filters
{
    public class ResultFilter<T>
    {
        public ResultFilter(T data, int statusCode, string error)
        {
            Data = data;
            this.StatusCode = statusCode;
            this.Errors = error;
        }


        public bool Succeeded {
            get {
                if (Errors != null) return false;
                else return true;} 
        }
        public string Errors { get; set; }
       // public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
