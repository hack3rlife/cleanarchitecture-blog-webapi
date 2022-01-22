using AutoMapper;
using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Profiles
{
    public class ProfileMapper: Profile
    {
        public ProfileMapper()
        {
            // Requests
            CreateMap<BlogAddRequestDto, Blog>();
            CreateMap<BlogUpdateRequestDto, Blog>();
            CreateMap<PostAddRequestDto, Post>();
            CreateMap<PostUpdateRequestDto, Post>();
            CreateMap<CommentAddRequestDto, Comment>();
            CreateMap<CommentUpdateRequestDto, Comment>();

            // Responses
            CreateMap<Blog, BlogResponseDto>();
            CreateMap<Blog, BlogDetailsResponseDto>();
            CreateMap<Post, PostDetailsResponseDto>();
            CreateMap<Post, PostResponseDto>();
            CreateMap<Comment, CommentDetailsResponseDto>();
            CreateMap<Comment, CommentResponseDto>();

        }
    }
}
