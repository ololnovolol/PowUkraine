using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Application.Services
{
    public class BLLMarkService : IBLLMarkService
    {
        private readonly IMapper _mapper;

        public BLLMarkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork _unitOfWork { get; }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<int> AddAsync(MarkBL markBl)
        {
            var mark = _mapper.Map<Mark>(markBl);

            return await _unitOfWork.Marks.AddAsync(mark);
        }

        public async Task<int> UpdateAsync(MarkBL markBl)
        {
            var mark = _mapper.Map<Mark>(markBl);

            return await _unitOfWork.Marks.UpdateAsync(mark);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _unitOfWork.Marks.DeleteAsync(id.ToString());
        }

        public IEnumerable<MarkBL> GetAll()
        {
            var list = new List<MarkBL>();

            foreach (var item in _unitOfWork.Marks.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<MarkBL>(item));
            }

            return list;
        }

        public MarkBL GetByMessage(Guid messageId)
        {
            var mark = _unitOfWork.Marks.GetByMessageIdAsync(messageId.ToString());

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetById(Guid id)
        {
            var mark = _unitOfWork.Marks.GetByIdAsync(id.ToString()).Result;

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetByMessageId(Guid messageId)
        {
            return _mapper.Map<MarkBL>(_unitOfWork.Marks.GetByMessageIdAsync(messageId.ToString()).Result);
        }

        public IEnumerable<MarkBL> GetByUser(Guid userId)
        {
            var messages = _unitOfWork.Messages.GetByUserIdAsync(userId.ToString()).Result;
            var list = new List<MarkBL>();
            foreach (var item in messages)
            {
                list.Add(_mapper.Map<MarkBL>(_unitOfWork.Marks.GetByMessageIdAsync(_mapper.Map<MessageBL>(item).Id.ToString())));
            }
            
            return list;
        }
    }
}