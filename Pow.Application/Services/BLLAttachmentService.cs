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
    public class BLLAttachmentService : IBLLAttachmentService
    {
        private readonly IMapper _mapper;

        public BLLAttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork UnitOfWork { get; }

        public void Dispose() => UnitOfWork.Dispose();

        public async Task<int> AddAsync(AttachmentBL attachmentBl)
        {
            Attachment attachment = _mapper.Map<Attachment>(attachmentBl);

            return await UnitOfWork.Attachments.AddAsync(attachment);
        }

        public async Task<int> UpdateAsync(AttachmentBL attachmentBl)
        {
            Attachment attachment = _mapper.Map<Attachment>(attachmentBl);

            return await UnitOfWork.Attachments.UpdateAsync(attachment);
        }

        public async Task<int> DeleteAsync(Guid id) => await UnitOfWork.Attachments.DeleteAsync(id.ToString());

        public async Task<IEnumerable<AttachmentBL>> GetAll()
        {
            List<AttachmentBL> list = new List<AttachmentBL>();

            foreach (Attachment item in await UnitOfWork.Attachments.GetAllAsync())
            {
                list.Add(_mapper.Map<AttachmentBL>(item));
            }

            return list;
        }

        public IEnumerable<AttachmentBL> GetByMessageId(string messageId)
        {
            List<AttachmentBL> list = new List<AttachmentBL>();

            foreach (Attachment mark in UnitOfWork.Attachments.GetByMessageIdAsync(messageId).Result)
            {
                list.Add(_mapper.Map<AttachmentBL>(mark));
            }

            return list;
        }

        public AttachmentBL GetById(Guid id)
        {
            Task<Attachment> attachment = UnitOfWork.Attachments.GetByIdAsync(id.ToString());

            return _mapper.Map<AttachmentBL>(attachment);
        }

        public IEnumerable<AttachmentBL> GetByUser(Guid userId)
        {
            IReadOnlyList<Message> messages = UnitOfWork.Messages.GetByUserIdAsync(userId.ToString()).Result;
            List<AttachmentBL> list = new List<AttachmentBL>();

            foreach (Message item in messages)
            {
                list.Add(
                    _mapper.Map<AttachmentBL>(
                        UnitOfWork.Attachments.GetByMessageIdAsync(_mapper.Map<MessageBL>(item).Id.ToString())));
            }

            return list;
        }

        public async Task<int> DeleteByMessageId(Guid messageId)
        {
            IReadOnlyList<Attachment> attachments = await UnitOfWork.Attachments.GetAllAsync();

            return attachments.Where(i => i.MessageId == messageId)
                .Select(async i => await UnitOfWork.Attachments.DeleteAsync(i.Id.ToString()))
                .Count();
        }
    }
}
