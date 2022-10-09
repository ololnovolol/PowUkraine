using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;

using Pow.WebApi.Controllers.Base;
using Pow.WebApi.Extensions;
using Pow.WebApi.Models;

namespace Pow.WebApi.Controllers
{
    public class MessageController : BaseController
    {
        private readonly IMapper _mapper;

        private readonly IBLLMessageService _messageService;

        private readonly IBLLService _service;

        public MessageController(
            IBLLService service,
            IMapper mapper,
            IBLLMessageService messageService)
        {
            _mapper = mapper;
            _messageService = messageService;
            _service = service;
        }

        // [Authorize(Policy = "UserAccess")]
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Get() => Ok("Users_only___User___get method=)");

        // [Authorize(Policy = "AdminAccess")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<MessageModel> messages =
                (await _messageService.GetAll()).Select(i => _mapper.Map<MessageModel>(i));

            return Ok(messages);
        }

        /*[Authorize(Roles = "Admin")]*/
        [HttpGet]
        public async Task<IActionResult> GetAllMessageWithMarks()
        {
            IEnumerable<MessageWithMarkModel> messagesWithMarks =
                (await _service.GetAllMessagesWithMarks()).Select(i => _mapper.Map<MessageWithMarkModel>(i));

            return Ok(messagesWithMarks);
        }

        [HttpPost]
        public async Task<IActionResult> Message(IFormCollection data)
        {
            MessageModel msg = new();
            MarkModel mark = null;
            AttachmentModel attachment = null;

            msg.Phone = data["PhoneNumber"];
            msg.EventDate = DateTime.Parse(data["Data"]);
            msg.Description = data["Description"];
            msg.Title = data["Title"];
            Guid.TryParse(data["UserId"], out Guid dataGuid);
            msg.UserId = dataGuid;

            if (data.Files.Count > 0)
            {
                attachment = new AttachmentModel
                {
                    Title = data.Files[0].FileName,
                    File = data.Files[0].GetBytes().Result,
                };
            }

            if (!data["Latitude"].Equals("0") && !data["Longitude"].Equals("0"))
            {
                mark = new MarkModel
                {
                    Disabled = false,
                    GpsLatitude = data["Latitude"],
                    GpsLongitude = data["Longitude"],
                    MapUrl = data["MapUrl"],
                };
            }

            await _service.AddAsync(
                _mapper.Map<MessageBL>(msg),
                _mapper.Map<AttachmentBL>(attachment),
                _mapper.Map<MarkBL>(mark));

            return Ok("Posted");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) => Ok(await _service.Delete(id));
    }
}
