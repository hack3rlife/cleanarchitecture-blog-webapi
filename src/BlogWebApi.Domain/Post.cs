using System;
using System.Collections.Generic;

namespace BlogWebApi.Domain
{
    public class Post : Audit
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public Guid PostId { get; set; }
        public string PostName { get; set; }
        public string Text { get; set; }
        public Guid BlogId { get; set; }

        public ICollection<Comment> Comment { get; }
    }
}
