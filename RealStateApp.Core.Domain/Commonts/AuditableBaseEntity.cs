using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Commonts
{
    public class AuditableBaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedby { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
