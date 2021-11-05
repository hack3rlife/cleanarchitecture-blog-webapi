using System;
using BlogWebApi.Domain;
using LoremNET;

namespace Application.UnitTest.Builders
{
    public class PostBuilder
    {
        private readonly Post _post;

        private PostBuilder()
        {
            _post = new Post();
        }

        public PostBuilder Create()
        {
            return new PostBuilder();
        }

        public PostBuilder WithId(Guid guid)
        {
            _post.PostId = guid;
            return this;
        }

        public PostBuilder WithName(string name = "What's Unit Testing?'")
        {
            _post.PostName = name;
            return this;
        }

        public PostBuilder WithText(string text)
        {
            _post.Text = text;
            return this;
        }

        public PostBuilder WithBlogId(Guid guid)
        {
            _post.BlogId = guid;
            return this;
        }

        public PostBuilder WithComments(int numComments)
        {
            for (var i = 0; i < numComments; i++)
            {
                var newComment = new Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentName = LoremNET.Lorem.Words(10),
                    Email = Lorem.Email(),
                    PostId = Guid.NewGuid()
                };

                this._post.Comment.Add(newComment);
            }

            return this;
        }

        public Post Build()
        {
            var post = new Post
            {
                PostId = _post.PostId,
                PostName = _post.PostName,
                Text = _post.Text,
                BlogId = _post.BlogId,
            };

            return post;
        }

        public static Post Default()
        {
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            return post;
        }

        public static Post WithoutName()
        {
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            return post;
        }

        public static Post WithoutText()
        {
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                BlogId = Guid.NewGuid()
            };

            return post;
        }

        public static Post WithoutBlogId()
        {
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10),
            };

            return post;
        }

        public static Post WithoutPostId()
        {
            var post = new Post
            {
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            return post;
        }

        public static Post WithLongName()
        {
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(50),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            return post;
        }
    }
}