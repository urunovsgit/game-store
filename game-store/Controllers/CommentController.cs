﻿using game_store.Models;
using game_store_business.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace game_store.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentServiceProvider;

        public CommentController(ICommentService commentServiceProvider)
        {
            _commentServiceProvider = commentServiceProvider;
        }

        [HttpPost]
        public async Task<int> AddComment(CommentViewModel commentModel)
        {
            var comment = await _commentServiceProvider.CreateAsync(commentModel);
            return comment.Id;
        }

        [HttpPost]
        public async Task DeleteComment(int commentId)
        {
            await _commentServiceProvider.DeleteByIdAsync(commentId);
        }
    }
}
