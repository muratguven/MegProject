
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
        
        [Inject]
        public IUnitOfWork _unitOfWork { private get; set; }

        
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

        

    }
}