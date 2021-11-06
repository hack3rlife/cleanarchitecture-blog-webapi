using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using Moq;

namespace Application.UnitTest.Mocks
{
    public class MockPostsRepository : Mock<IPostRepository>
    {
        /// <summary>
        /// Setup the mock to return a List
        /// </summary>
        /// <param name="returnPosts">Specifies the value to return after the setup </param>
        /// <returns><see cref="List{Post}"/></returns>
        public async Task<MockPostsRepository> MockSetupListAllAsync(List<Post> returnPosts)
        {
            Setup(x => x.ListAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(await Task.Run(() => returnPosts))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// Verify that the mock is called <para>times</para> using <paramref name="skip"/> and <paramref name="take"/>
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="times"></param>
        /// <returns>MockPostsRepository</returns>
        public MockPostsRepository MockVerifyListAllAsync(int skip, int take, Times times)
        {
            Verify(mock => mock.ListAllAsync(skip, take), times);
            return this;
        }

        /// <summary>
        /// Setup the mock to return a single <see cref="Post"/> without comments
        /// </summary>
        /// <param name="post">The <see cref="Post"/> to be returned by the mock</param>
        /// <returns>A <see cref="Post"/> object</returns>
        public async Task<MockPostsRepository> MockSetupGetByIdAsync(Post post)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(await Task.Run(() => post))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// Verify that the mock executes <see cref="Post.GetByIdAsync(System.Guid)"/> <paramref name="times"/>
        /// </summary>
        /// <param name="post"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockPostsRepository MockVerifyGetByIdAsync(Post post, Times times)
        {
            Verify(mock => mock.GetByIdAsync(post.PostId), times);
            return this;
        }

        /// <summary>
        /// Setup the mock to return a <see cref="Post"/> with its comments.
        /// </summary>
        /// <param name="returnPosts"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>A <see cref="Post"/> object</returns>
        public async Task<MockPostsRepository> MockSetupGetByIdWithCommentsTask(Post returnPosts, int skip, int take)
        {
            Setup(x => x.GetByIdWithCommentsAsync(It.Is<Guid>(p => p == returnPosts.PostId), skip, take))
                .ReturnsAsync(await Task.Run(() => returnPosts))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockPostsRepository MockVerifyGetByIdWithPostsAsync(Guid postId, int skip, int take, Times times)
        {
            Verify(mock => mock.GetByIdWithCommentsAsync(postId, skip, take), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockPostsRepository MockSetupAdd()
        {
            Setup(x => x.AddAsync(It.IsAny<Post>())).Verifiable();
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="returnPost"></param>
        /// <returns></returns>
        public async Task<MockPostsRepository> MockSetupAddAsync(Post returnPost)
        {
            Setup(x => x.AddAsync(It.IsAny<Post>()))
                .ReturnsAsync(await Task.Run(() => returnPost))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPost"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockPostsRepository MockVerifyAddAsync(Post newPost, Times times)
        {
            Verify(mock => mock.AddAsync(newPost), times);
            return this;
        }

        /// <summary>
        /// Setup the mock for <see cref="IPostRepository"/>.UpdateAsync method
        /// </summary>
        /// <returns></returns>
        public MockPostsRepository MockSetupUpdateAsync()
        {
            Setup(x => x.UpdateAsync(It.IsAny<Post>()))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// Verify that was called <paramref name="times"/>
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockPostsRepository MockVerifyUpdateAsync(Post updatedPost, Times times)
        {
            Verify(mock => mock.UpdateAsync(updatedPost), times);
            return this;
        }

        /// <summary>
        /// Mock Setup"/>
        /// </summary>
        /// <returns></returns>
        public MockPostsRepository MockSetupDeleteAsync()
        {
            Setup(x => x.DeleteAsync(It.IsAny<Post>())).Verifiable();
            return this;
        }

        /// <summary>
        /// Verifies <seePostdbody.core.Entities.Post)"/>
        /// was called <param name="times"></param> using <param name="post"></param>
        /// </summary>
        /// <param name="post"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockPostsRepository MockVerifyDeleteAsync(Post post, Times times)
        {
            Verify(mock => mock.DeleteAsync(post), times);
            return this;
        }
    }
}
