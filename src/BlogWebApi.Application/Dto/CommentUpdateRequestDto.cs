using System;

namespace BlogWebApi.Application.Dto
{
    public class CommentUpdateRequestDto
    {
        public Guid CommentId { get; set; }
        public string CommentName { get; set; }
        public string Email { get; set; }
        public Guid PostId { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
