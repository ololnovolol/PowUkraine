using System;
using Dapper;
using Pow.Infrastructure;

namespace Pow.WebApi.Extensions
{
    public static class DapperExtensions
    {
        public static void AddSqlGuidHandler()
        {
            SqlMapper.AddTypeHandler(new SqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
        }
    }
}
