using System;

namespace BlogWebApi.Application.Dto
{
    public class BlogByIdResponseDto
    {
        public Guid BlogId { get; set; }
        public string BlogName { get; set; }
    }
}