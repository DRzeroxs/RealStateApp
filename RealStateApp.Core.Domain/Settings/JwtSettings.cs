using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Settings
{
    public class JwtSettings
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
