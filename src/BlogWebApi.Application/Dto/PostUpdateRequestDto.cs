using System;

namespace BlogWebApi.Application.Dto
{
    public class PostUpdateRequestDto
    {
        public Guid PostId { get; set; }
        public string PostName { get; set; }
        public string Text { get; set; }
        public Guid BlogId { get; set; }
        public string UpdatedBy { get; set; }
    }
}
