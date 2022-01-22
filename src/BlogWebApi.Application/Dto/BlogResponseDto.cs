using System;

namespace BlogWebApi.Application.Dto
{
    public class BlogResponseDto
    {
        public Guid BlogId { get; set; }
        public string BlogName { get; set; }
    }
}