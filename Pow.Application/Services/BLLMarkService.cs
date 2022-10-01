using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pow.Application.Models;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Application.Services
{
    public class BLLMarkService : IDisposable
    {
        private readonly IMapper _mapper;

        public BLLMarkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork _unitOfWork { get; }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<int> Add(MarkBL markBl)
        {
            var mark = _mapper.Map<Mark>(markBl);

            return await _unitOfWork.Marks.AddAsync(mark);
        }

        public async Task<int> Update(MarkBL markBl)
        {
            var mark = _mapper.Map<Mark>(markBl);

            return await _unitOfWork.Marks.UpdateAsync(mark);
        }

        public async Task<int> Delete(string id)
        {
            return await _unitOfWork.Marks.DeleteAsync(id);
        }

        public IEnumerable<MarkBL> GetAll()
        {
            var list = new List<MarkBL>();

            foreach (var item in _unitOfWork.Marks.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<MarkBL>(item));
            }

            return list;
        }

        public MarkBL GetByMessage(string messageId)
        {
            var mark = _unitOfWork.Marks.GetByMessageIdAsync(messageId);

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetById(string id)
        {
            var mark = _unitOfWork.Marks.GetByIdAsync(id).Result;

            return _mapper.Map<MarkBL>(mark);
        }
    }
}