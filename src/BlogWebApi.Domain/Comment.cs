using System;

namespace BlogWebApi.Domain
{
    public class Comment : Audit
    {
        public Guid CommentId { get; set; }
        public string CommentName { get; set; }
        public string Email { get; set; }
        public Guid PostId { get; set; }
    }
}
