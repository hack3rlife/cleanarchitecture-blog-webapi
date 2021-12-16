using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.WebApi.Controllers
{
    /// <summary>
    /// Manages Comments CRUD Actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="commentService"></param>
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> of <see cref="Comment"/>
        /// </summary>
        /// <response code="200">Returns a list of existing comments</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IAsyncEnumerable<CommentResponseDto>>> Get([FromQuery] int skip, int take)
        {
            var comments = await _commentService.GetAll(skip, take);

            return Ok(comments);
        }

        /// <summary>
        /// Gets a single comment details
        /// </summary>
        /// <param name="commentId">Blog's guid</param>
        /// <response code="200">When a valid <see cref="Guid"/> value is passed, the related Blog details will be returned.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <response code="404">When the provided <see cref="Guid"/> does not exist or is not related any <see cref="Comment"/> in the database.</response>
        [HttpGet("{commentId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDetailsResponseDto>> Get(Guid commentId)
        {
            var comment = await _commentService.GetBy(commentId);

            if (comment == null)
                return NotFound(commentId);

            return Ok(comment);
        }

        /// <summary>
        /// Adds a new comment
        /// </summary>
        /// <param name="comment"></param>
        /// <response code="201">When the <see cref="Comment"/> is added successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentResponseDto>> Post([FromBody] CommentAddRequestDto comment)
        {
            var newComment = await _commentService.Add(comment);

            if (newComment == null)
                return BadRequest();

            return CreatedAtAction(nameof(Post), newComment);
        }

        /// <summary>
        /// Updates an existing comment
        /// </summary>
        /// <param name="comment"></param>
        /// <response code="204">When the comment is updated successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] CommentUpdateRequestDto comment)
        {
            await _commentService.Update(comment);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes an existing comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <response code="204">When the comment is deleted successfully.</response>
        /// <response code="400">When there is an error during the execution of the request.</response>
        /// <returns></returns>
        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid commentId)
        {
            await _commentService.Delete(commentId);

            return NoContent();
        }
    }
}