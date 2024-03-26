using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dto.Account
{
    public class JwtResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
