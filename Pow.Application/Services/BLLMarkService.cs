using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Application.Services
{
    public class BLLMarkService : IDisposable
    {
        private IUnitOfWork _unitOfWork { get; }

        private readonly IMapper _mapper;

        public BLLMarkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<int> Add(MarkBL markBl)
        {
            var mark = this._mapper.Map<Mark>(markBl);
            return await this._unitOfWork.Marks.AddAsync(mark);
        }

        public async Task<int> Update(MarkBL markBl)
        {
            var mark = this._mapper.Map<Mark>(markBl);
            return await this._unitOfWork.Marks.UpdateAsync(mark);
        }

        public async Task<int> Delete(string id)
        {
            return await this._unitOfWork.Marks.DeleteAsync(id);
        }

        public IEnumerable<MarkBL> GetAll()
        {
            List<MarkBL> list = new List<MarkBL>();
            foreach (Mark item in this._unitOfWork.Marks.GetAllAsync().Result)
            {
                list.Add(this._mapper.Map<MarkBL>(item));
            }

            return list;
        }

        public void Dispose()
        {
            this._unitOfWork.Dispose();
        }
    }
}
