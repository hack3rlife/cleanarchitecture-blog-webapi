using System;

namespace BlogWebApi.Application.Dto
{
    public class PostAddRequestDto
    {
        public Guid PostId { get; set; }
        public string PostName { get; set; }
        public string Text { get; set; }
        public Guid BlogId { get; set; }
        public string CreatedBy { get; set; }
    }
}
