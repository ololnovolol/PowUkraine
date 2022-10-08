using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
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

        public void Get() => throw new NotImplementedException();

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _bLLAttachmentService.Dispose();
                _bLLMarkService.Dispose();
                _bLLMessageService.Dispose();
            }

            _disposed = true;
        }

        public async Task<IEnumerable<MessageMarkBL>> GetAllMessagesWithMarks()
        {
            var messages = await _bLLMessageService.GetAll();
            var marks = await _bLLMarkService.GetAll();
            var messageMarks = new List<MessageMarkBL>();
            foreach (var message in messages)
            {
                MessageMarkBL messageMark = new MessageMarkBL()
                {
                    Id = message.Id,
                    Description = message.Description,
                    Title = message.Title,
                    EventDate = message.EventDate,
                    CreatedDate = message.CreatedDate,
                    Marked = marks.Count(i => i.MessageId == message.Id),
                    UserId = message.UserId,
                };

                messageMarks.Add(messageMark);
            }

            return messageMarks;
        }

        public void GetByUserId(Guid userId) => throw new NotImplementedException();

        public async Task<int> Delete(Guid messageId)
        {
            await _bLLMarkService.DeleteByMessageId(messageId);
            await _bLLAttachmentService.DeleteByMessageId(messageId);
            return await _bLLMessageService.DeleteAsync(messageId);
        }
    }
}
