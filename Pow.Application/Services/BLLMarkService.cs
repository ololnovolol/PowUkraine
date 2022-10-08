using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> DeleteAsync(Guid id) => await UnitOfWork.Marks.DeleteAsync(id.ToString());

        public async Task<IEnumerable<MarkBL>> GetAll()
        {
            List<MarkBL> list = new List<MarkBL>();

            foreach (Mark item in await UnitOfWork.Marks.GetAllAsync())
            {
                list.Add(_mapper.Map<MarkBL>(item));
            }

            return list;
        }

        public MarkBL GetById(Guid id)
        {
            Mark mark = UnitOfWork.Marks.GetByIdAsync(id.ToString()).Result;

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetByMessageId(Guid messageId)
            => _mapper.Map<MarkBL>(UnitOfWork.Marks.GetByMessageIdAsync(messageId.ToString()).Result);

        public IEnumerable<MarkBL> GetByUser(Guid userId)
        {
            IReadOnlyList<Message> messages = UnitOfWork.Messages.GetByUserIdAsync(userId.ToString()).Result;
            List<MarkBL> list = new List<MarkBL>();

            foreach (Message item in messages)
            {
                list.Add(
                    _mapper.Map<MarkBL>(
                        UnitOfWork.Marks.GetByMessageIdAsync(_mapper.Map<MessageBL>(item).Id.ToString())));
            }

            return list;
        }

        public async Task<int> DeleteByMessageId(Guid messageId)
        {
            IReadOnlyList<Mark> marks = await UnitOfWork.Marks.GetAllAsync();

            return marks.Where(i => i.MessageId == messageId)
                .Select(async i => await UnitOfWork.Marks.DeleteAsync(i.Id.ToString()))
                .Count();
        }

        public MarkBL GetByMessage(Guid messageId)
        {
            Task<Mark> mark = UnitOfWork.Marks.GetByMessageIdAsync(messageId.ToString());

            return _mapper.Map<MarkBL>(mark);
        }
    }
}
