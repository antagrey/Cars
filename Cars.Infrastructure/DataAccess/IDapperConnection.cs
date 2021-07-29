using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cars.Infrastructure.DataAccess
{
    public interface IDapperConnection : IDisposable
    {
        void Open();
        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            CommandType commandType,
            object param = null);
    }
}
