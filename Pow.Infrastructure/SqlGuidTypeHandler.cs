using System;
using System.Data;
using Dapper;

namespace Pow.Infrastructure
{
    public class SqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }
    }
}
