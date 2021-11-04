using System;
using System.Collections.Generic;

namespace BlogWebApi.Domain
{
    public class Blog : Audit
    {
        public Blog()
        {
            Post = new HashSet<Post>();
        }

        public Guid BlogId { get; set; }
        public string BlogName { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}
