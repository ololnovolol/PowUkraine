using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;

namespace Pow.Application.Services
{
    public class BLLService : IBLLService
    {
        private readonly IBLLAttachmentService bLLAttachmentService;

        private readonly IBLLMarkService bLLMarkService;

        private readonly IBLLMessageService bLLMessageService;

        private bool disposed = false;

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

        ~BLLService()
        {
            Dispose(false);
        }

        public BLLService(IBLLMessageService messageService, IBLLMarkService markService, IBLLAttachmentService attachmentService)
        {
            bLLAttachmentService = attachmentService;
            bLLMarkService = markService;
            bLLMessageService = messageService;
        }

        public async Task<int> AddAsync(MessageBL message, AttachmentBL? attachment, MarkBL? mark)
        {
            message.Id = Guid.NewGuid();
            message.CreatedDate = DateTime.Now;
            await bLLMessageService.AddAsync(message);

            if (attachment != null)
            {
                attachment.MessageId = message.Id;
                await bLLAttachmentService.AddAsync(attachment);
            }

            if (mark != null)
            {
                mark.MessageId = message.Id;
                await bLLMarkService.AddAsync(mark);
            }

            return 1;

        }

        public void Get(out MessageBL message, out AttachmentBL attachment, out MarkBL mark)
        {
            throw new NotImplementedException();
        }

        public void Get(out Task<MessageBL> message, out Task<AttachmentBL> attachment, out Task<MarkBL> mark) => throw new NotImplementedException();

        public void GetAll(out Task<IEnumerable<MessageBL>> messages, out Task<IEnumerable<AttachmentBL>> attachments, out Task<IEnumerable<MarkBL>> marks) => throw new NotImplementedException();

        public void GetByUserId(out Task<IEnumerable<MessageBL>> messages, out Task<IEnumerable<AttachmentBL>> attachments, out Task<IEnumerable<MarkBL>> marks) => throw new NotImplementedException();
    }
}
