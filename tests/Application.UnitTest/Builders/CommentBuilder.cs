using System;
using BlogWebApi.Domain;
using LoremNET;

namespace Application.UnitTest.Builders
{
    public class CommentBuilder
    {
        private readonly Comment _comment;

        private CommentBuilder()
        {
            _comment = new Comment();
        }

        public static CommentBuilder Create()
        {
            return new CommentBuilder();
        }

        

        /// <summary>
        /// Creates a <see cref="Comment"/> with required fields
        /// </summary>
        /// <returns>A <see cref="Comment"/> object</returns>
        public static Comment Default()
        {
            return new Comment
            {
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(10),
                Email = Lorem.Email(),
                PostId = Guid.NewGuid()
            };
        }

        public CommentBuilder WithRandomGuid()
        {
            _comment.CommentId = Guid.NewGuid();
            return this;
        }

        public CommentBuilder WithEmptyGuid()
        {
            _comment.CommentId = Guid.Empty;
            return this;
        }

        public CommentBuilder WithGuid(Guid guid)
        {
            _comment.CommentId = guid;
            return this;
        }

        public CommentBuilder WithRandomEmail()
        {
            _comment.Email = Lorem.Email();
            return this;
        }

        public CommentBuilder WithEmail(string email)
        {
            _comment.Email = email;
            return this;
        }

        public CommentBuilder WithPostId(Guid guid)
        {
            _comment.PostId = guid;
            return this;
        }

        public CommentBuilder WithRandomPostId()
        {
            _comment.PostId = Guid.NewGuid();
            return this;
        }

        public Comment Build()
        {
            return new Comment
            {
                CommentId = _comment.CommentId,
                CommentName = _comment.CommentName,
                Email = _comment.Email,
                PostId = _comment.PostId
            };
        }

        public CommentBuilder WithRandomCommandName()
        {
            _comment.CommentName = Lorem.Words(50);
            return this;
        }

        public CommentBuilder WithCommandName(string commandName)
        {
            _comment.CommentName = commandName;
            return this;
        }
    }
}
