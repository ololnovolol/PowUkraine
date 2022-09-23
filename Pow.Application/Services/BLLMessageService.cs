using System;
using System.Collections.Generic;
using AutoMapper;
using Pow.Application.Models;
using Pow.Infrastructure.Repositories;

namespace Pow.Application.Services
{
    public class BLLMessageService : IDisposable
    {
        private UnitOfWork DB { get; }

        public BLLMessageService()
        {
            DB = new UnitOfWork();
        }


        public bool AddUser(MessageBL element)
        {
            DB.Users.Create(item: Mapper.Map<User>(element));
            DB.Save();
            return true;
        }

        public void UpdateUser(MessageBL element)
        {
            var toUpdate = DB.Users.Read(Id: Guid.Parse(element.Id)).Result;

            if (toUpdate != null)
            {
                toUpdate = Mapper.Map<User>(element);
                DB.Users.Update(toUpdate);
                DB.Save();
            }
        }

        public void RemoveUser(Guid Id)
        {
            DB.Users.Delete(Id);
            DB.Save();
        }

        public IEnumerable<UserBL> GetUsers()
        {
            List<UserBL> result = new List<UserBL>();

            foreach (var item in DB.Users.ReadAll())
                result.Add(item: Mapper.Map<UserBL>(item));

            return result;
        }

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
