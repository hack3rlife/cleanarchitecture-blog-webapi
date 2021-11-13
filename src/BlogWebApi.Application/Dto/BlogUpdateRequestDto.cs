using System;

namespace BlogWebApi.Application.Dto
{
    public class BlogUpdateRequestDto
    {
        public Guid BlogId { get; set; }
        public string BlogName { get; set; }
        public string UpdatedBy { get; set; }
    }
}
