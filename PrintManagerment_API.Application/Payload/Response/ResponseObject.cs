using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Payload.Response
{
    public class ResponseObject<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ResponseObject() { }
        public ResponseObject(int status, string message, T? data) 
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
