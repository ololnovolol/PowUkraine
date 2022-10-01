﻿using Microsoft.AspNetCore.Authentication;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Application.Services
{
    public class BLLService
    {
        private readonly IBLLAttachmentService bLLAttachmentService;

        private readonly IBLLMarkService bLLMarkService;

        private readonly IBLLMessageService bLLMessageService;

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
    }
}
