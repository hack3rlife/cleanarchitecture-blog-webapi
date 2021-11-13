using System;

namespace BlogWebApi.Application.Dto
{
    public class BlogAddRequestDto 
    {
        public Guid BlogId { get; set; }
        public string BlogName { get; set; }
        public string CreatedBy { get; set; }
    }
}
