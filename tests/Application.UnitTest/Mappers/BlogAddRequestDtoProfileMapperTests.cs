using AutoMapper;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Profiles;
using BlogWebApi.Domain;
using LoremNET;
using System;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class BlogAddRequestDtoProfileMapperTests : ProfileMapperTestBase
    {

        [Fact]
        public void BlogMapper_FromBlogAddRequestToBlog_Success()
        {
            //Arrange
            var blogAddRequestDto = new BlogAddRequestDto
            {
                BlogName = "BlogName",
                BlogId = Guid.NewGuid(),
                CreatedBy = "UnitTest"
            };

            //Act
            var blog = _mapper.Map<Blog>(blogAddRequestDto);

            //Assert
            Assert.Equal(blogAddRequestDto.BlogName, blog.BlogName);
            Assert.Equal(blogAddRequestDto.BlogId, blog.BlogId);
            Assert.Equal(blogAddRequestDto.CreatedBy, blog.CreatedBy);
        }

       

      

       
    }
}
