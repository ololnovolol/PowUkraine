using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Application.Services
{
    public class BLLAttachmentService : IDisposable
    {
        private readonly IMapper _mapper;

        public BLLAttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork UnitOfWork { get; }

        public void Dispose() => UnitOfWork.Dispose();

        public async Task<int> Add(AttachmentBL attachmentBl)
        {
            Attachment attachment = _mapper.Map<Attachment>(attachmentBl);

            return await UnitOfWork.Attachments.AddAsync(attachment);
        }

        public async Task<int> Update(AttachmentBL attachmentBl)
        {
            Attachment attachment = _mapper.Map<Attachment>(attachmentBl);

            return await UnitOfWork.Attachments.UpdateAsync(attachment);
        }

        public async Task<int> Delete(string id) => await UnitOfWork.Attachments.DeleteAsync(id);

        public IEnumerable<AttachmentBL> GetAll()
        {
            List<AttachmentBL> list = new();

            foreach (Attachment item in UnitOfWork.Attachments.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<AttachmentBL>(item));
            }

            return list;
        }

        public IEnumerable<AttachmentBL> GetByMessageId(string messageId)
        {
            List<AttachmentBL> list = new();

            foreach (Attachment mark in UnitOfWork.Attachments.GetByMessageIdAsync(messageId).Result)
            {
                list.Add(_mapper.Map<AttachmentBL>(mark));
            }

            return list;
        }

        public AttachmentBL GetById(string id)
        {
            Task<Attachment> attachment = UnitOfWork.Attachments.GetByIdAsync(id);

            return _mapper.Map<AttachmentBL>(attachment);
        }
    }
}
