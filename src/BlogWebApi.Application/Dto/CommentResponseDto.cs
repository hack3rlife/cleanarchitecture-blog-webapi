using System;

namespace BlogWebApi.Application.Dto
{
    public class CommentResponseDto
    {
        public Guid CommentId { get; set; }
        public string CommentName { get; set; }
        public Guid PostId { get; set; }

    }
}
