
using System.Reflection;
using AutoMapper;
using log4net;

namespace MegProject.Business.Core
{
    public abstract class ApplicationCore:IApplicationCore
    {
         /* 
          * 
          * Apllication Domain genelinde yapılacak işlemler için bu kısma methodlar yazılacaktır.
          * ?*/

       //protected ILog log { get; set; }
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