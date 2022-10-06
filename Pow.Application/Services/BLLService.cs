using System;
using System.Threading.Tasks;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;

namespace Pow.Application.Services
{
    public class BLLService : IBLLService
    {
        private readonly IBLLAttachmentService _bLLAttachmentService;

        private readonly IBLLMarkService _bLLMarkService;

        private readonly IBLLMessageService _bLLMessageService;

        private bool _disposed = false;

        public BLLService(IBLLMessageService messageService, IBLLMarkService markService, IBLLAttachmentService attachmentService)
        {
            _bLLAttachmentService = attachmentService;
            _bLLMarkService = markService;
            _bLLMessageService = messageService;
        }

        ~BLLService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> AddAsync(MessageBL message, AttachmentBL? attachment, MarkBL? mark)
        {
            message.Id = Guid.NewGuid();
            message.CreatedDate = DateTime.Now;
            await _bLLMessageService.AddAsync(message);

            if (attachment != null)
            {
                attachment.MessageId = message.Id;
                await _bLLAttachmentService.AddAsync(attachment);
            }

            if (mark != null)
            {
                mark.MessageId = message.Id;
                await _bLLMarkService.AddAsync(mark);
            }

            return 1;
        }

        public void Get(out MessageBL message, out AttachmentBL attachment, out MarkBL mark)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
            }

            _disposed = true;
        }
    }
}
