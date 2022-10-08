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
    public class BllMessageService : IBLLMessageService
    {
        private readonly IMapper _mapper;

        private bool _disposed;

        public BllMessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        ~BllMessageService() => Dispose(false);

        private IUnitOfWork UnitOfWork { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> AddAsync(MessageBL messageBl)
        {
            Message message = _mapper.Map<Message>(messageBl);

            return await UnitOfWork.Messages.AddAsync(message);
        }

        public async Task<int> UpdateAsync(MessageBL messageBl)
        {
            Message message = _mapper.Map<Message>(messageBl);

            return await UnitOfWork.Messages.UpdateAsync(message);
        }

        public async Task<int> DeleteAsync(Guid id) => await UnitOfWork.Messages.DeleteAsync(id.ToString());

        public async Task<IEnumerable<MessageBL>> GetAll()
        {
            List<MessageBL> list = new();

            foreach (Message item in await UnitOfWork.Messages.GetAllAsync())
            {
                list.Add(_mapper.Map<MessageBL>(item));
            }

            return list;
        }

        public MessageBL GetById(Guid id)
        {
            Message message = UnitOfWork.Messages.GetByIdAsync(id.ToString()).Result;

            return _mapper.Map<MessageBL>(message);
        }

        public IEnumerable<MessageBL> GetByUser(Guid userId)
        {
            List<MessageBL> list = new();

            foreach (Message item in UnitOfWork.Messages.GetByUserIdAsync(userId.ToString()).Result)
            {
                list.Add(_mapper.Map<MessageBL>(item));
            }

            return list;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            _disposed = true;
        }
    }
}
