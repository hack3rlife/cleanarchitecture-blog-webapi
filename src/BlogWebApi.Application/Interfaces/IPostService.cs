using BlogWebApi.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponseDto>> GetAll(int skip = 0, int take = 10);
        Task<PostDetailsResponseDto> GetBy(Guid postId);
        Task<PostDetailsResponseDto> GetCommentsBy(Guid postId, int skip = 0, int take = 10);
        Task<PostResponseDto> Add(PostAddRequestDto post);
        Task Update(PostUpdateRequestDto post);
        Task Delete(Guid postId);
    }
}