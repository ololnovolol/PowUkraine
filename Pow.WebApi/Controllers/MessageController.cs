using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Models;
using Pow.Application.Services;
using Pow.Application.Services.Interfaces;
using Pow.Domain;
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
        public IActionResult GetAll(){
            MessageModel msg = new MessageModel();
            MarkModel mark = null;
            AttachmentModel attachment = null;
            /*_service.GetAll(out );*/
            return Ok();
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

            if (true) // TODO Check is mark empty?
            {
                mark = new MarkModel();
                mark.Disabled = false;
                mark.GpsLatitude = data["Latitude"];
                mark.GpsLongitude = data["Longitude"];
                mark.MapUrl = data["MapUrl"];
            }

            await _service.AddAsync(_mapper.Map<MessageBL>(msg), _mapper.Map<AttachmentBL>(attachment), _mapper.Map<MarkBL>(mark));

            return Ok();
        }



        [Authorize(Policy = "UserAccess")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) => Ok($"delete{id}");
    }
}
