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
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork UnitOfWork { get; }

        public void Dispose() => UnitOfWork.Dispose();

        public async Task<int> Add(MarkBL markBl)
        {
            Mark mark = _mapper.Map<Mark>(markBl);

            return await UnitOfWork.Marks.AddAsync(mark);
        }

        public async Task<int> Update(MarkBL markBl)
        {
            Mark mark = _mapper.Map<Mark>(markBl);

            return await UnitOfWork.Marks.UpdateAsync(mark);
        }

        public async Task<int> Delete(string id) => await UnitOfWork.Marks.DeleteAsync(id);

        public IEnumerable<MarkBL> GetAll()
        {
            List<MarkBL> list = new();

            foreach (Mark item in UnitOfWork.Marks.GetAllAsync().Result)
            {
                list.Add(_mapper.Map<MarkBL>(item));
            }

            return list;
        }

        public MarkBL GetByMessage(string messageId)
        {
            Task<Mark> mark = UnitOfWork.Marks.GetByMessageIdAsync(messageId);

            return _mapper.Map<MarkBL>(mark);
        }

        public MarkBL GetById(string id)
        {
            Mark mark = UnitOfWork.Marks.GetByIdAsync(id).Result;

            return _mapper.Map<MarkBL>(mark);
        }
    }
}
