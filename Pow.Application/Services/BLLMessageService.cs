using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Application.Services
{
    public class BLLMessageService : IDisposable
    {
        private readonly IMapper _mapper;

        public BLLMessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork UnitOfWork { get; }

        public void Dispose() => UnitOfWork.Dispose();

        public async Task<int> Add(MessageBL messageBl)
        {
            Message message = _mapper.Map<Message>(messageBl);

            return await UnitOfWork.Messages.AddAsync(message);
        }

        public async Task<int> Update(MessageBL messageBl)
        {
            Message message = _mapper.Map<Message>(messageBl);

            return await UnitOfWork.Messages.UpdateAsync(message);
        }

        public async Task<int> Delete(string id) => await UnitOfWork.Messages.DeleteAsync(id);

        public IEnumerable<MessageBL> GetAll()
        {
            List<MessageBL> list = new();

            foreach (Message item in UnitOfWork.Messages.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<MessageBL>(item));
            }

            return list;
        }

        public MessageBL GetById(string id)
        {
            Message message = UnitOfWork.Messages.GetByIdAsync(id).Result;

            return _mapper.Map<MessageBL>(message);
        }

        public IEnumerable<MessageBL> GetByUser(string userId)
        {
            List<MessageBL> list = new();

            foreach (Message item in UnitOfWork.Messages.GetByUserIdAsync(userId).Result)
            {
                list.Add(_mapper.Map<MessageBL>(item));
            }

            return list;
        }
    }
}
