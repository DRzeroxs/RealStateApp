using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Exceptions
{
    public class ApiEception : Exception
    {
        public int ErrorCode { get; set; }
        public ApiEception() : base()
        {

        }

        public ApiEception(string message) : base(message)
        {

        }
        public ApiEception(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiEception(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
