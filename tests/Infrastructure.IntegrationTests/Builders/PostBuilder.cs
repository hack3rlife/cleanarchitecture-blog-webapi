using System;
using System.Collections.Generic;
using System.Linq;
using BlogWebApi.Domain;
using LoremNET;

namespace Infrastructure.IntegrationTests.Builders
{
    public class PostBuilder
    {
        private Guid _postId { get; set; }
        private string _postName { get; set; }
        private string _text { get; set; }
        private Guid _blogId { get; set; }
        private ICollection<Comment> Comment { get; }

        public PostBuilder()
        {
            Comment = new HashSet<Comment>();
        }
        public static PostBuilder Create()
        {
            return new PostBuilder();
        }

        public PostBuilder WithId(Guid guid)
        {
            this._postId = guid;
            return this;
        }

        public PostBuilder WithName(string name = "What's Unit Testing?'")
        {
            this._postName = name;
            return this;
        }

        public PostBuilder WithText(string text)
        {
            this._text = text;
            return this;
        }

        public PostBuilder WithBlogId(Guid guid)
        {
            this._blogId = guid;
            return this;
        }

        public Post Build()
        {
            var post = new Post
            {
                PostId = _postId,
                PostName = this._postName,
                Text = this._text,
                BlogId = this._blogId,
            };

            Comment.ToList().ForEach(x => post.Comment.Add(x));

            return post;
        }

        public static Post Default()
        {
            return new Post
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(100),
                BlogId = Guid.NewGuid()
            };
        }

        public PostBuilder WithPostId(Guid guid)
        {
            _postId = guid;
            return this;
        }
    }
}