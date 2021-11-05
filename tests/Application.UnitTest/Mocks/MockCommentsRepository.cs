using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using Moq;

namespace Application.UnitTest.Mocks
{
    public class MockCommentsRepository : Mock<ICommentRepository>
    {
        /// <summary>
        /// Setup the mock to return a List
        /// </summary>
        /// <param name="returnBlogs">Specifies the value to return after the setup </param>
        /// <returns><see cref="List{T}"/></returns>
        public async Task<MockCommentsRepository> MockSetupListAllAsync(List<Comment> returnBlogs)
        {
            Setup(x => x.ListAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(await Task.Run(() => returnBlogs))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockCommentsRepository MockVerifyListAllAsync(int skip, int take, Times times)
        {
            Verify(mock => mock.ListAllAsync(skip, take), times);
            return this;
        }

        /// <summary>
        /// Setup the mock to return a single <see cref="Post"/> without comments
        /// </summary>
        /// <param name="comment">The <see cref="Post"/> to be returned by the mock</param>
        /// <returns>A <see cref="Post"/> object</returns>
        public async Task<MockCommentsRepository> MockSetupGetByIdAsync(Comment comment)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(await Task.Run(() => comment))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// Verify that the mock executes <see cref="Comment.GetByIdAsync(System.Guid)"/> <paramref name="times"/>
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockCommentsRepository MockVerifyGetByIdAsync(Comment comment, Times times)
        {
            Verify(mock => mock.GetByIdAsync(comment.CommentId), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockCommentsRepository MockSetupAdd()
        {
            Setup(x => x.AddAsync(It.IsAny<Comment>()))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// Setup mock for AddAsync comments operation
        /// </summary>
        /// <param name="returnsComment"></param>
        /// <returns>A <see cref="Comment"/> object</returns>
        public async Task<MockCommentsRepository> MockSetupAddAsync(Comment returnsComment)
        {
            Setup(x => x.AddAsync(It.IsAny<Comment>()))
                .ReturnsAsync(await Task.Run(() => returnsComment))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newComment"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockCommentsRepository MockVerifyAddAsync(Comment newComment, Times times)
        {
            Verify(mock => mock.AddAsync(newComment), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockCommentsRepository MockSetupUpdateAsync()
        {
            Setup(x => x.UpdateAsync(It.IsAny<Comment>()))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockCommentsRepository MockVerifyUpdateAsync(Times times)
        {
            Verify(mock => mock.UpdateAsync(It.IsAny<Comment>()), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockCommentsRepository MockSetupDeleteAsync()
        {
            Setup(x => x.DeleteAsync(It.IsAny<Comment>())).Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockCommentsRepository MockVerifyDeleteAsync(Times times)
        {
            Verify(mock => mock.DeleteAsync(It.IsAny<Comment>()), times);
            return this;
        }
    }
}