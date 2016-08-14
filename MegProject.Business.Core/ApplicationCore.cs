
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using log4net;
using MegProject.Data.Core;
using Ninject;

namespace MegProject.Business.Core
{
    public abstract class ApplicationCore:IApplicationCore
    {

        private IUnitOfWork _unitOfWork;

        
        protected ILog log
        {
            get
            {
                ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                log4net.Config.XmlConfigurator.Configure();
                return Log;
            }
        }

        public IMapper Mapper
        {
            get
            {
                IMapper mapper = Dto.DtoMap.Map();
                return mapper;
            }
        }

        #region CRUD Methods

        public List<T> Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> where) where T : class
        {
            _unitOfWork = new UnitOfWork();
            var result = _unitOfWork.GetRepository<T>().FindList(where);
            return result.ToList();
        }

        public List<T> GetAll<T>() where T:class
        {
            _unitOfWork = new UnitOfWork();
            var result = _unitOfWork.GetRepository<T>().GetAll();
            return result.ToList();
        }

        public T Add<T>(T data) where T : class
        {
            _unitOfWork = new UnitOfWork();
            var result = _unitOfWork.GetRepository<T>().Add(data);
            return result;
        }

        public T Update<T>(T data) where T : class
        {
            _unitOfWork = new UnitOfWork();
            var result = _unitOfWork.GetRepository<T>().Update(data);
            return result;
        }

        #endregion





    }
}