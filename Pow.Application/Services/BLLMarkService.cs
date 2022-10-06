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
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork UnitOfWork { get; }

        public void Dispose() => UnitOfWork.Dispose();

        public async Task<int> AddAsync(MarkBL markBl)
        {
            Mark mark = _mapper.Map<Mark>(markBl);

            return await UnitOfWork.Marks.AddAsync(mark);
        }

        public async Task<int> UpdateAsync(MarkBL markBl)
        {
            Mark mark = _mapper.Map<Mark>(markBl);

            return await UnitOfWork.Marks.UpdateAsync(mark);
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
            Task<Mark> mark = UnitOfWork.Marks.GetByMessageIdAsync(messageId);

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetById(string id)
        {
            Mark mark = UnitOfWork.Marks.GetByIdAsync(id).Result;

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
