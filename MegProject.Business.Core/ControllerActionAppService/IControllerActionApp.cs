using System.Collections.Generic;
using MegProject.Business.Core;
using MegProject.Dto;

namespace MegProject.Business.Core.ControllerActionAppService
{
    public interface IControllerActionApp:IApplicationCore
    {
        /// <summary>
        /// Yeni Controller ve bağlı action'ları veri tabanına kayıt eder.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="actionList"></param>
        /// <returns></returns>
        bool CreateControllerAction(DtoSystemControllers controller, List<DtoSystemActions> actionList);

        /// <summary>
        /// İsme göre Controller Id getirir.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        int FindController(string Name);

        /// <summary>
        /// İsme göre Action Id getirir.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        int FindAction(string Name);

        /// <summary>
        /// Tüm Controllerları Listeler.
        /// </summary>
        /// <returns></returns>
        List<DtoSystemControllers> GetAllControllers();

        /// <summary>
        /// Db de  olmayan Controller ve Action satırlarını siler.
        /// Dikkat Otomatik olarak eklenen Controller ve Action Temizleme işlemi yapar.
        /// DEĞİŞİKLİKLER VERİLERİ GERİ ALINAMAYACAK ŞEKİLDE SİLER.
        /// </summary>
        /// <param name="newControllers"></param>
        /// <param name="newActions"></param>
        void ClearControllerActions(DtoSystemControllers newControllers, List<DtoSystemActions> newActions);

        /// <summary>
        /// role Id ye göre roleaction listesini getirir.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<DtoRoleAction> GetSelectedRoleActions(int? roleId);

    }
}