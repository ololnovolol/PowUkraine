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
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork _unitOfWork { get; }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<int> Add(MessageBL messageBl)
        {
            var message = _mapper.Map<Message>(messageBl);

            return await _unitOfWork.Messages.AddAsync(message);
        }

        public async Task<int> Update(MessageBL messageBl)
        {
            var message = _mapper.Map<Message>(messageBl);

            return await _unitOfWork.Messages.UpdateAsync(message);
        }

        public async Task<int> Delete(string id)
        {
            return await _unitOfWork.Messages.DeleteAsync(id);
        }

        public IEnumerable<MessageBL> GetAll()
        {
            var list = new List<MessageBL>();

            foreach (var item in _unitOfWork.Messages.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<MessageBL>(item));
            }

            return list;
        }

        public MessageBL GetById(string id)
        {
            var message = _unitOfWork.Messages.GetByIdAsync(id).Result;

            return _mapper.Map<MessageBL>(message);
        }

        public IEnumerable<MessageBL> GetByUser(string userId)
        {
            var list = new List<MessageBL>();

            foreach (var item in _unitOfWork.Messages.GetByUserIdAsync(userId).Result)
            {
                list.Add(_mapper.Map<MessageBL>(item));
            }

            return list;
        }
    }
}