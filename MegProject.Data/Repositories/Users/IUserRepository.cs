using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using MegProject.Data.Core;
using System.Linq;

namespace MegProject.Data.Repositories.Users
{
    public interface IUserRepository:IGenericRepository<Data.Users>
    {
        DataTable GetAllObjects();
    }
}