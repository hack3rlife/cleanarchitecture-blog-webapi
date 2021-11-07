using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.WebApi.Controllers
{
    /// <summary>
    /// Manages CRUD Actions for Posts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postService"></param>
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/Posts
        /// <summary>
        /// Gets a list of posts
        /// </summary>
        /// <response code="200">Returns a list of existing posts</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IAsyncEnumerable<Post>>> Get([FromQuery] int skip, int take)
        {
            return Ok(await _postService.GetAll(skip, take));
        }

        // GET: api/Posts/5
        /// <summary>
        /// Gets a single post details
        /// </summary>
        /// <param name="postId"></param>
        /// <response code="200">When a valid <see cref="Guid"/> value is passed, the related Blog details will be returned.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <response code="404">When the provided <see cref="Guid"/> does not exist or is not related any <see cref="Blog"/> in the database.</response>
        /// <returns></returns>
        [HttpGet("{postId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Post>> Get(Guid postId)

        {
            var blog = await _postService.GetBy(postId);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        /// <summary>
        /// Gets a single post details
        /// </summary>
        /// <param name="postId">Blog's guid</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <response code="200">When a valid <see cref="Guid"/> value is passed, the related Blog details will be returned.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <response code="404">When the provided <see cref="Guid"/> does not exist or is not related any <see cref="Blog"/> in the database.</response>
        /// <returns></returns>
        [HttpGet("GetCommentsBy/{postId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Post>> Get(Guid postId, [FromQuery] int skip, int take)
        {
            var blog = await _postService.GetCommentsBy(postId, skip, take);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // POST: api/Posts
        /// <summary>
        /// Adds a new post
        /// </summary>
        /// <param name="post"></param>
        /// <response code="201">When the post is added successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Post>> Post([FromBody] Post post)
        {
            var newPost = await _postService.Add(post);

            if (newPost == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Post), newPost);

        }

        // PUT: api/Posts/5
        /// <summary>
        /// Updates an existing post
        /// </summary>
        /// <param name="post"></param>
        /// <response code="204">When the post is updated successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] Post post)
        {
            await _postService.Update(post);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes an existing post
        /// </summary>
        /// <param name="postId"></param>
        /// <response code="204">When the post is deleted successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpDelete("{postId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid postId)
        {
            await _postService.Delete(postId);

            return NoContent();
        }
    }
}