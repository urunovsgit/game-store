using AutoMapper;
using Data.Interfaces;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using game_store_domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.ServiceProviders
{
    public class CommentServiceProvider : ICommentService
    {
        private readonly IUnitOfWork _gsUnitOfWork;
        private readonly IMapper _mapperProfile;

        public CommentServiceProvider(IUnitOfWork unitOfWork, IMapper mapperProfile)
        {
            _gsUnitOfWork = unitOfWork;
            _mapperProfile = mapperProfile;
        }

        public async Task<IEnumerable<CommentModel>> GetAllAsync()
        {
            var comments = await _gsUnitOfWork.CommentRepository.GetAllAsync();
            return _mapperProfile.Map<IEnumerable<CommentModel>>(comments);
        }

        public async Task<CommentModel> GetByIdAsync(int id)
        {
            var comment = await _gsUnitOfWork.CommentRepository.GetByIdAsync(id);
            return _mapperProfile.Map<CommentModel>(comment);
        }

        public async Task<CommentModel> CreateAsync(CommentModel modelDTO)
        {
            var commentDAO = _mapperProfile.Map<Comment>(modelDTO);

            var instance = await _gsUnitOfWork.CommentRepository.AddAsync(commentDAO);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<CommentModel>(instance);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _gsUnitOfWork.CommentRepository.DeleteByIdAsync(id);
            await _gsUnitOfWork.SaveAsync();
        }

        public async Task<CommentModel> RestoreCommentAsync(int id)
        {
            var comment = await _gsUnitOfWork.CommentRepository.GetByIdAsync(id);
            comment.IsDeleted = false;

            _gsUnitOfWork.CommentRepository.Update(comment);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<CommentModel>(comment);
        }

        public async Task<CommentModel> UpdateAsync(CommentModel modelDTO)
        {
            var comment = _mapperProfile.Map<Comment>(modelDTO);
            _gsUnitOfWork.CommentRepository.Update(comment);
            await _gsUnitOfWork.SaveAsync();

            comment = await _gsUnitOfWork.CommentRepository.GetByIdAsync(comment.Id);
            return _mapperProfile.Map<CommentModel>(comment);
        }
    }
}
