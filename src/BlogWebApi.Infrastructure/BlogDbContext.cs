using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BlogWebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Infrastructure
{
    public class BlogDbContext : DbContext
    {
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Post> Post { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
            SavedChanges += SavedChangesHandler;
            SaveChangesFailed += SaveChangesFailedHandler;
        }

        private void SaveChangesFailedHandler(object? sender, SaveChangesFailedEventArgs e)
        {
            Debug.WriteLine(e.Exception);

        }

        public static void SavedChangesHandler(object sender, SavedChangesEventArgs e)
        {
            Debug.WriteLine(e.EntitiesSavedCount);
        }

        public void Seed()
        {
           var blogs = new List<Blog>
            {
                new Blog
                {
                    BlogId = new Guid("c949db94-5195-498a-afbe-7a90031b3125"),
                    BlogName = "Asp. Net Core Development & Testing"
                }
            };

            var posts = new List<Post>
            {
                new Post
                {
                    BlogId = new Guid("c949db94-5195-498a-afbe-7a90031b3125"),
                    PostName = "Asp. Net Core Clean Architecture Testing",
                    PostId = new Guid("b7713508-4162-4f0b-a258-e46ae97ac40a")
                }
            };

          var comments = new List<Comment>
            {
                new Comment
                {
                    CommentId = new Guid("ffe18dc4-d4b9-4504-be02-886f11af7c02"),
                    CommentName = "This is Awesome!!!",
                    Email = "me@hack3rlife.io",
                    PostId = new Guid("b7713508-4162-4f0b-a258-e46ae97ac40a")
                }
            };

            // Seed, if necessary
            if (!this.Blog.Any())
            {
                this.Blog.AddRange(blogs);
                this.Post.AddRange(posts);
                this.Comment.AddRange(comments);
            }
        }
    }
}