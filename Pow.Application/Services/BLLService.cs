using Microsoft.AspNetCore.Authentication;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<int> Add(MessageBL message, AttachmentBL? attachment, MarkBL? mark)
        {
            message.Id = Guid.NewGuid();
            message.CreatedDate = DateTime.Now;
            
            mark.Id = Guid.NewGuid();
            mark.MessageId = message.Id;

            attachment.Id = Guid.NewGuid();
            attachment.MessageId = message.Id;
            

            await bLLMarkService.Add(mark);
            await bLLAttachmentService.Add(attachment);
            return await bLLMessageService.Add(message);

        }              

        public void Get(out MessageBL message, out AttachmentBL attachment, out MarkBL mark)
        {
            throw new NotImplementedException();
        }
    }
}
