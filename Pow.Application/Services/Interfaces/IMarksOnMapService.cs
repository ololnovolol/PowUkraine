using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IMarksOnMapService
    {
        Task<IEnumerable<MarkOnMap>> GetAllAsync();
    }
}
