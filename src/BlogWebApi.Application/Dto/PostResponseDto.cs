using System;

namespace BlogWebApi.Application.Dto
{
    public class PostResponseDto
    {
        public Guid PostId { get; set; }
        public string PostName { get; set; }
        public Guid BlogId { get; set; }

    }
}
