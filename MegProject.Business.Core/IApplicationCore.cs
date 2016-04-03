using AutoMapper;
using log4net;

namespace MegProject.Business.Core
{
    public interface IApplicationCore
    {
        /// <summary>
        /// AutoMapper Create Map:
        /// </summary>
        IMapper Mapper { get;}
    }
}