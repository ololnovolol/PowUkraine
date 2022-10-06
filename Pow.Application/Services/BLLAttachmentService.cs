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
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork UnitOfWork { get; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public async Task<int> AddAsync(AttachmentBL attachmentBl)
        {
            var attachment = _mapper.Map<Attachment>(attachmentBl);

            return await UnitOfWork.Attachments.AddAsync(attachment);
        }

        public async Task<int> UpdateAsync(AttachmentBL attachmentBl)
        {
            var attachment = _mapper.Map<Attachment>(attachmentBl);

            return await UnitOfWork.Attachments.UpdateAsync(attachment);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await UnitOfWork.Attachments.DeleteAsync(id.ToString());
        }

        public IEnumerable<AttachmentBL> GetAll()
        {
            var list = new List<AttachmentBL>();

            foreach (var item in UnitOfWork.Attachments.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<AttachmentBL>(item));
            }

            return list;
        }

        public IEnumerable<AttachmentBL> GetByMessageId(string messageId)
        {
            var list = new List<AttachmentBL>();

            foreach (var mark in UnitOfWork.Attachments.GetByMessageIdAsync(messageId).Result)
            {
                list.Add(_mapper.Map<AttachmentBL>(mark));
            }

            return list;
        }

        public AttachmentBL GetById(Guid id)
        {
            var attachment = UnitOfWork.Attachments.GetByIdAsync(id.ToString());

            return _mapper.Map<AttachmentBL>(attachment);
        }                     

        public IEnumerable<AttachmentBL> GetByUser(Guid userId)
        {
            var messages = UnitOfWork.Messages.GetByUserIdAsync(userId.ToString()).Result;
            var list = new List<AttachmentBL>();
            foreach (var item in messages)
            {
                list.Add(_mapper.Map<AttachmentBL>(UnitOfWork.Attachments.GetByMessageIdAsync(_mapper.Map<MessageBL>(item).Id.ToString())));
            }

            return list;
        }
    }
}
