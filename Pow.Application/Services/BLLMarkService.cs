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
            return await UnitOfWork.Marks.DeleteAsync(id.ToString());
        }

        public IEnumerable<MarkBL> GetAll()
        {
            var list = new List<MarkBL>();

            foreach (var item in UnitOfWork.Marks.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<MarkBL>(item));
            }

            return list;
        }

        public MarkBL GetByMessage(Guid messageId)
        {
            Task<Mark> mark = UnitOfWork.Marks.GetByMessageIdAsync(messageId.ToString());

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetById(Guid id)
        {
            Mark mark = UnitOfWork.Marks.GetByIdAsync(id.ToString()).Result;

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetByMessageId(Guid messageId)
        {
            return _mapper.Map<MarkBL>(UnitOfWork.Marks.GetByMessageIdAsync(messageId.ToString()).Result);
        }

        public IEnumerable<MarkBL> GetByUser(Guid userId)
        {
            var messages = UnitOfWork.Messages.GetByUserIdAsync(userId.ToString()).Result;
            var list = new List<MarkBL>();
            foreach (var item in messages)
            {
                list.Add(_mapper.Map<MarkBL>(UnitOfWork.Marks.GetByMessageIdAsync(_mapper.Map<MessageBL>(item).Id.ToString())));
            }

            return list;
        }
    }
}
