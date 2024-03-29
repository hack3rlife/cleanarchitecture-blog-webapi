﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewRelic.Api.Agent;

namespace BlogWebApi.WebApi.Controllers
{
    /// <summary>
    /// Manages Blog CRUD Actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogService"></param>
        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// Gets a list of blogs in a paginated fashion
        /// </summary>
        /// <response code="200">Returns a list of existing blogs</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Trace]
        public async Task<ActionResult<IAsyncEnumerable<BlogResponseDto>>> Get([FromQuery]int skip, int take)
        {
            return Ok(await _blogService.GetAll(skip, take));
        }

        /// <summary>
        /// Gets a single blog details
        /// </summary>
        /// <response code="200">When a valid <see cref="Guid"/> value is passed, the related Blog details will be returned.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <response code="404">When the provided <see cref="Guid"/> does not exist or is not related any <see cref="Blog"/> in the database.</response>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Trace]
        public async Task<ActionResult<BlogResponseDto>> Get(Guid guid, [FromQuery] int skip = 0, int take = 10)
        {
            var blog = await _blogService.GetPostsBy(guid, skip, take);

            if (blog == null)
            {
                return NotFound(guid);
            }

            return Ok(blog);
        }


        /// <summary>
        /// Adds a new blog
        /// </summary>
        /// <param name="blogAddRequestDto"></param>
        /// <response code="201">When the blog is added successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Trace]
        public async Task<ActionResult<BlogResponseDto>> Add([FromBody] BlogAddRequestDto blogAddRequestDto)
        {
            var newBlog = await _blogService.Add(blogAddRequestDto);

            if (newBlog == null)
            {
                return BadRequest(blogAddRequestDto);
            }

            return CreatedAtAction(nameof(Add), newBlog);
        }

        /// <summary>
        /// Updates an existing blog
        /// </summary>
        /// <param name="blogUpdateRequestDto"></param>
        /// <response code="204">When the blog is updated successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Trace]
        public async Task<IActionResult> Put([FromBody] BlogUpdateRequestDto blogUpdateRequestDto)
        {
            await _blogService.Update(blogUpdateRequestDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing blog
        /// </summary>
        /// <param name="guid"></param>
        /// <response code="204">When the blog is deleted successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpDelete("{guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Trace]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _blogService.Delete(guid);

            return NoContent();
        }
    }
}