using System;
using System.Collections.Generic;
using AutoMapper;
using log4net;
using MegProject.Data.Core;

namespace MegProject.Business.Core
{
    public interface IApplicationCore
    {
        List<T> Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> where) where T : class;
        List<T> GetAll<T>() where T : class;
        T Add<T>(T data) where T : class;
        T Update<T>(T data) where T : class;
    }
}