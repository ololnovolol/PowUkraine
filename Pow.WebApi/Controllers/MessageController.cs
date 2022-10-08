﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;
using Pow.Infrastructure.Repositories.Interfaces;
using Pow.WebApi.Controllers.Base;
using Pow.WebApi.Extensions;
using Pow.WebApi.Models;

namespace Pow.WebApi.Controllers
{
    public class MessageController : BaseController
    {

        private readonly IMapper _mapper;

        private readonly IBLLAttachmentService _attachmentService;

        private readonly IBLLMarkService _markService;

        private readonly IBLLMessageService _messageService;

        private readonly IBLLService _service;

        private readonly IUnitOfWork _unitOfWork;

        public MessageController(IBLLService service, IMapper mapper, IBLLMessageService messageService, IBLLMarkService markService, IBLLAttachmentService attachmentService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _messageService = messageService;
            _markService = markService;
            _attachmentService = attachmentService;
            _service = service;
            _unitOfWork = unitOfWork;
        }

        // [Authorize(Policy = "UserAccess")]
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Get() => Ok("Users_only___User___get method=)");

        // [Authorize(Policy = "AdminAccess")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            IEnumerable<MessageModel> messages = (await _messageService.GetAll()).Select(i => _mapper.Map<MessageModel>(i));

            return Ok(messages);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllMessageWithMarks()
        {
            IEnumerable<MessageWithMarkModel> messagesWithMarks = (await _service.GetAllMessagesWithMarks()).Select(i => _mapper.Map<MessageWithMarkModel>(i));

            return Ok(messagesWithMarks);
        }


        [HttpPost]
        public async Task<IActionResult> Message(IFormCollection data/* , IFormFile imagefile*/)
        {
            MessageModel msg = new MessageModel();
            MarkModel mark = null;
            AttachmentModel attachment = null;

            msg.Phone = data["PhoneNumber"];
            msg.EventDate = DateTime.Parse(data["Data"]);
            msg.Description = data["Description"];
            msg.Title = data["Title"];

            if (data.Files.Count > 0)
            {
                attachment = new AttachmentModel();
                attachment.Title = data.Files[0].FileName;
                attachment.File = data.Files[0].GetBytes().Result;
            }

            if (!data["Latitude"].Equals("0") && !data["Longitude"].Equals("0"))
            {
                mark = new MarkModel();
                mark.Disabled = false;
                mark.GpsLatitude = data["Latitude"];
                mark.GpsLongitude = data["Longitude"];
                mark.MapUrl = data["MapUrl"];
            }

            await _service.AddAsync(_mapper.Map<MessageBL>(msg), _mapper.Map<AttachmentBL>(attachment), _mapper.Map<MarkBL>(mark));

            return Ok("Posted");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}
