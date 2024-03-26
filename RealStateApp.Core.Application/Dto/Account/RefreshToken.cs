using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dto.Account
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool isExpired { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string ReplaceByToken { get; set; }
        public bool IsActive => Revoked == null && !isExpired;
    }
}
