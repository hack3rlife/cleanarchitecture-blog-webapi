using System;
using System.Collections.Generic;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Dto
{
    public class BlogDetailsResponseDto
    {
        public Guid BlogId { get; set; }
        public string BlogName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}