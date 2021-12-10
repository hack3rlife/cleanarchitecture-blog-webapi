using System;
using System.Collections.Generic;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Dto
{
    public class PostDetailsResponseDto
    {
        public Guid PostId { get; set; }
        public string PostName { get; set; }
        public string Text { get; set; }
        public Guid BlogId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<Comment> Comment { get; set; }
    }
}
