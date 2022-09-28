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
        private IUnitOfWork _unitOfWork { get; }

        private readonly IMapper _mapper;

        public BLLAttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<int> Add(AttachmentBL attachmentBl)
        {
            var attachment = this._mapper.Map<Attachment>(attachmentBl);
            return await this._unitOfWork.Attachments.AddAsync(attachment);
        }

        public async Task<int> Update(AttachmentBL attachmentBl)
        {
            var attachment = this._mapper.Map<Attachment>(attachmentBl);
            return await this._unitOfWork.Attachments.UpdateAsync(attachment);
        }

        public async Task<int> Delete(string id)
        {
            return await this._unitOfWork.Attachments.DeleteAsync(id);
        }

        public IEnumerable<AttachmentBL> GetAll()
        {
            List<AttachmentBL> list = new List<AttachmentBL>();
            foreach (Attachment item in this._unitOfWork.Attachments.GetAllAsync().Result)
            {
                list.Add(this._mapper.Map<AttachmentBL>(item));
            }

            return list;
        }

        public void Dispose()
        {
            this._unitOfWork.Dispose();
        }
    }
}
