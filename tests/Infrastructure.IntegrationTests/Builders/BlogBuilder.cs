using System;
using BlogWebApi.Domain;
using LoremNET;

namespace Infrastructure.IntegrationTests.Builders
{
    public class BlogBuilder
    {
        private readonly Blog _blog;

        private BlogBuilder()
        {
            _blog = new Blog
            {
                BlogId = Guid.NewGuid(), 
                BlogName = Lorem.Words(20)
                
            };
        }

        public static BlogBuilder Create()
        {
            return new BlogBuilder();
        }

        public BlogBuilder WithId(Guid guid)
        {
            _blog.BlogId = guid;
            return this;
        }

        public BlogBuilder WithEmptyBlogId()
        {
            _blog.BlogId = Guid.Empty;
            return this;
        }

        public BlogBuilder WithName(string name)
        {
            _blog.BlogName = name;
            return this;
        }

        public BlogBuilder WithEmptyName()
        {
            _blog.BlogName = string.Empty;
            return this;
        }

        public BlogBuilder WithPost(Post post)
        {
            this._blog.Post.Add(post);
            return this;
        }

        public BlogBuilder WithPosts(int n)
        {
            for (var i = 0; i < n; i++)
            {
                var post =
                    new Post
                    {

                        PostId = Guid.NewGuid(),
                        PostName = Lorem.Words(20),
                        Text = Lorem.Paragraph(100,1000,5,10),
                        BlogId = _blog.BlogId

                    };

                this._blog.Post.Add(post);

            }

            return this;
        }

        public Blog Build()
        {
            return _blog;
        }

        public static Blog Default()
        {
            return new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = Guid.NewGuid()
            };
        }
    }
}
