using Application.UnitTest.Mocks;
using AutoMapper;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Profiles;
using BlogWebApi.Application.Services;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceBase
    {
        public readonly MockCommentsRepository _mockCommentRepository;
        public readonly ICommentService _commentService;

        public const int skip = 0; //default pagination value
        public const int take = 10; //default pagination value

        public CommentServiceBase()
        {
            var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMapper>(); });

            var mapper = configurationProvider.CreateMapper();

            _mockCommentRepository = new MockCommentsRepository();
            _commentService = new CommentService(_mockCommentRepository.Object, mapper);
        }
    }
}
