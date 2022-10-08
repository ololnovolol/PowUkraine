#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;

namespace Pow.Application.Services
{
    public class BllService : IBllService
    {
        private readonly IBLLAttachmentService _bLlAttachmentService;

        private readonly IBLLMarkService _bLlMarkService;

        private readonly IBLLMessageService _bLlMessageService;

        private bool _disposed;

        public BllService(
            IBLLMessageService messageService,
            IBLLMarkService markService,
            IBLLAttachmentService attachmentService)
        {
            _bLlAttachmentService = attachmentService;
            _bLlMarkService = markService;
            _bLlMessageService = messageService;
        }

        ~BllService() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> AddAsync(MessageBL message, AttachmentBL? attachment, MarkBL? mark)
        {
            message.Id = Guid.NewGuid();
            message.CreatedDate = DateTime.Now;
            await _bLlMessageService.AddAsync(message);

            if (attachment != null)
            {
                attachment.MessageId = message.Id;
                await _bLlAttachmentService.AddAsync(attachment);
            }

            if (mark == null)
            {
                return 1;
            }

            mark.MessageId = message.Id;
            await _bLlMarkService.AddAsync(mark);

            return 1;
        }

        public void Get() => throw new NotImplementedException();

        public async Task<IEnumerable<MessageMarkBl>> GetAllMessagesWithMarks()
        {
            IEnumerable<MessageBL> messages = await _bLlMessageService.GetAll();
            IEnumerable<MarkBL> marks = await _bLlMarkService.GetAll();

            return messages.Select(
                    message => new MessageMarkBl
                    {
                        Id = message.Id,
                        Description = message.Description,
                        Title = message.Title,
                        EventDate = message.EventDate,
                        CreatedDate = message.CreatedDate,
                        Marked = marks.Count(i => i.MessageId == message.Id),
                        UserId = message.UserId ?? Guid.Empty,
                    })
                .ToList();
        }

        public void GetByUserId(Guid userId) => throw new NotImplementedException();

        public async Task<int> Delete(Guid messageId)
        {
            await _bLlMarkService.DeleteByMessageId(messageId);
            await _bLlAttachmentService.DeleteByMessageId(messageId);

            return await _bLlMessageService.DeleteAsync(messageId);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _bLlAttachmentService.Dispose();
                _bLlMarkService.Dispose();
                _bLlMessageService.Dispose();
            }

            _disposed = true;
        }
    }
}
