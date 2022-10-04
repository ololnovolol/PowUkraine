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
    public class BLLAttachmentService : IBLLAttachmentService
    {
        private readonly IMapper _mapper;

        public BLLAttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork _unitOfWork { get; }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<int> Add(AttachmentBL attachmentBl)
        {
            var attachment = _mapper.Map<Attachment>(attachmentBl);

            return await _unitOfWork.Attachments.AddAsync(attachment);
        }

        public async Task<int> Update(AttachmentBL attachmentBl)
        {
            var attachment = _mapper.Map<Attachment>(attachmentBl);

            return await _unitOfWork.Attachments.UpdateAsync(attachment);
        }

        public async Task<int> Delete(Guid id)
        {
            return await _unitOfWork.Attachments.DeleteAsync(id.ToString());
        }

        public IEnumerable<AttachmentBL> GetAll()
        {
            var list = new List<AttachmentBL>();

            foreach (var item in _unitOfWork.Attachments.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<AttachmentBL>(item));
            }

            return list;
        }

        public IEnumerable<AttachmentBL> GetByMessageId(string messageId)
        {
            var list = new List<AttachmentBL>();

            foreach (var mark in _unitOfWork.Attachments.GetByMessageIdAsync(messageId).Result)
            {
                list.Add(_mapper.Map<AttachmentBL>(mark));
            }

            return list;
        }

        public AttachmentBL GetById(Guid id)
        {
            var attachment = _unitOfWork.Attachments.GetByIdAsync(id.ToString());

            return _mapper.Map<AttachmentBL>(attachment);
        }                     

        public IEnumerable<AttachmentBL> GetByUser(Guid userId)
        {
            var messages = _unitOfWork.Messages.GetByUserIdAsync(userId.ToString()).Result;
            var list = new List<AttachmentBL>();
            foreach (var item in messages)
            {
                list.Add(_mapper.Map<AttachmentBL>(_unitOfWork.Attachments.GetByMessageIdAsync(_mapper.Map<MessageBL>(item).Id.ToString())));
            }

            return list;
        }
    }
}