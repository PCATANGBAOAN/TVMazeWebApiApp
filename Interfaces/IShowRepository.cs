using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TVMazeWebAPIApp.Interfaces
{
    public interface IShowRepository
    {
        Task<DataTable> GetShowWithCast(int pageStart, int pageEnd);
        Task<DataTable> GetShowWithCast(int id);

    }
}
