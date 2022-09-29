using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;
using Pow.Infrastructure.Repositories;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Application.Services
{
    public class BLLMessageService : IDisposable
    {
        private IUnitOfWork _unitOfWork { get; }

        private readonly IMapper _mapper;

        public BLLMessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<int> Add(MessageBL messageBl)
        {
            var message = this._mapper.Map<Message>(messageBl);
            return await this._unitOfWork.Messages.AddAsync(message);
        }

        public async Task<int> Update(MessageBL messageBl)
        {
            var message = this._mapper.Map<Message>(messageBl);
            return await this._unitOfWork.Messages.UpdateAsync(message);
        }

        public async Task<int> Delete(string id)
        {
            return await this._unitOfWork.Messages.DeleteAsync(id);
        }

        public IEnumerable<MessageBL> GetAll()
        {
            List<MessageBL> list = new List<MessageBL>();
            foreach (Message item in this._unitOfWork.Messages.GetAllAsync().Result)
            {
                list.Add(this._mapper.Map<MessageBL>(item));
            }

            return list;
        }

        public MessageBL GetById(string id)
        {
            var message = this._unitOfWork.Messages.GetByIdAsync(id).Result;
            return this._mapper.Map<MessageBL>(message);
        }

        public IEnumerable<MessageBL> GetByUser(string userId)
        {
            List<MessageBL> list = new List<MessageBL>();
            foreach (Message item in this._unitOfWork.Messages.GetByUserIdAsync(userId).Result)
            {
                list.Add(this._mapper.Map<MessageBL>(item));
            }

            return list;
        }

        public void Dispose()
        {
            this._unitOfWork.Dispose();
        }
    }
}
