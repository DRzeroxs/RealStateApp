using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Wrappers
{
    public class Response<T>
    {
        public bool Suceded { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public Response() { }
        public Response(T data, string message = null)
        {
            Suceded = true;
            Message = message ?? "";
            Data = data;
        }
        public Response(string? message)
        {
            Suceded = false;
            Message = message;
        }
    }
}
