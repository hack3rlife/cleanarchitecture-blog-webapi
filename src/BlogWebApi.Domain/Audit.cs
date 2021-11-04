using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Domain
{
    public class Audit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CreatedBy { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastUpdate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string UpdatedBy { get; set; }

    }
}