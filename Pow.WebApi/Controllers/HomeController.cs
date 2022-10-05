using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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
    public class HomeController : BaseController
    {
        private readonly IMapper _mapper;

        private readonly IBLLAttachmentService _attachmentService;

        private readonly IBLLMarkService _markService;

        private readonly IBLLMessageService _messageService;

        private readonly IBLLService _service;

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IBLLService service, IMapper mapper, IBLLMessageService messageService, IBLLMarkService markService, IBLLAttachmentService attachmentService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _messageService = messageService;
            _markService = markService;
            _attachmentService = attachmentService;
            _service = service;
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Policy = "UserAccess")]
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Users_only___User___get method=)");
        }

        //[Authorize(Policy = "AdminAccess")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Admin Access_only___Admin___getAll");
        }

        [HttpPost]
        public async Task<IActionResult> Message(IFormCollection data/* , IFormFile imagefile*/)
        {
            Message mg = new Message()
            {
                Id = Guid.NewGuid(),
                Phone = data["PhoneNumber"],
                EventDate = DateTime.Parse(data["Data"]),
                Description = data["Description"],
                Title = data["Title"],
                CreatedDate = DateTime.Now
            };

            await _unitOfWork.Messages.AddAsync(mg);
            
            /*MessageModel msg = new MessageModel();
            MarkModel mark = null;
            AttachmentModel attachment = null;

            msg.Phone = data["PhoneNumber"];
            msg.EventDate = DateTime.Parse(data["Data"]);
            msg.Description = data["Description"];
            msg.Title = data["Title"];
            *//*UserManager<> manager = new UserManager();*//*
            if (data.Files.Count > 0)
            {
                attachment = new AttachmentModel();
                attachment.Title = data.Files[0].FileName;
                attachment.File = data.Files[0].GetBytes().Result;
            }

            if (true) //TODO Check is mark empty?
            {
                mark = new MarkModel();
                mark.Disabled = false;
                mark.GpsLatitude = data["Latitude"];
                mark.GpsLongitude = data["Longitude"];
                mark.MapUrl = data["MapUrl"];
            }

            await _service.AddAsync(_mapper.Map<MessageBL>(msg), _mapper.Map<AttachmentBL>(attachment), _mapper.Map<MarkBL>(mark));*/

            return Ok();
        }

        [Authorize(Policy = "UserAccess")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok($"delete{id}");
        }
    }
}