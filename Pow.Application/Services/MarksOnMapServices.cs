using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Application.Services
{
    public class MarksOnMapServices : IMarksOnMapService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarksOnMapServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MarkOnMap>> GetAllAsync()
        {
            var marks = await _unitOfWork.Marks.GetAllAsync();

            var marksOnMap = new List<MarkOnMap>();

            foreach (var mark in marks)
            {
                var relatedMessage = await _unitOfWork.Messages
                    .GetByIdAsync(mark.MessageId.ToString());

                marksOnMap.Add(new MarkOnMap(
                    mark.Id,
                    relatedMessage.Title,
                    relatedMessage.Description,
                    mark.GpsLatitude,
                    mark.GpsLongitude));
            }

            return marksOnMap;
        }
    }
}
