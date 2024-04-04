using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Exceptions
{
    public class ApiExeption : Exception
    {
        public int ErrorCode { get; set; }
        public ApiExeption() : base()
        {

        }

        public ApiExeption(string message) : base(message)
        {

        }
        public ApiExeption(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiExeption(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
