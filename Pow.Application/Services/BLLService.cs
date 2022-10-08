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
                bLLAttachmentService.Dispose();
                bLLMarkService.Dispose();
                bLLMessageService.Dispose();
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

        public void Get() => throw new NotImplementedException();

        public async Task<IEnumerable<MessageMarkBL>> GetAllMessagesWithMarks()
        {
            var messages = await bLLMessageService.GetAll();
            var marks = await bLLMarkService.GetAll();
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

        public void GetByUserId(Guid UserId) => throw new NotImplementedException();

        public async Task<int> Delete(Guid messageId)
        {
            await bLLMarkService.DeleteByMessageId(messageId);
            await bLLAttachmentService.DeleteByMessageId(messageId);
            return await bLLMessageService.DeleteAsync(messageId);
        }
    }
}
