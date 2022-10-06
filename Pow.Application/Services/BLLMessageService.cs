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
    public class BLLMessageService : IBLLMessageService
    {
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; }

        private bool disposed = false;

        public BLLMessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
             
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
            
            }
            disposed = true;
        }
        ~BLLMessageService()
        {
            Dispose(false);
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

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _unitOfWork.Messages.DeleteAsync(id.ToString());
        }

        public IEnumerable<MessageBL> GetAll()
        {
            List<MessageBL> list = new();

            foreach (Message item in UnitOfWork.Messages.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<MessageBL>(item));
            }

            return list;
        }

        public MessageBL GetById(Guid id)
        {
            Message message = UnitOfWork.Messages.GetByIdAsync(id).Result;

            return _mapper.Map<MessageBL>(message);
        }

        public IEnumerable<MessageBL> GetByUser(Guid userId)
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
