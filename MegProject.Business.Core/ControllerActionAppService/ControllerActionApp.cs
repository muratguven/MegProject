using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MegProject.Business.Core;
using MegProject.Data;
using MegProject.Data.Core;
using MegProject.Data.Core.Base;
using MegProject.Data.Models;
using MegProject.Data.Repositories;
using MegProject.Dto;
using Ninject;

namespace MegProject.Business.Core.ControllerActionAppService
{
    public class ControllerActionApp:ApplicationCore,IControllerActionApp
    {
      
        [Inject]
        public IUnitOfWork _unitofwork { private get; set; }
        

        public ControllerActionApp()
        {
            _unitofwork = new UnitOfWork();

        }


        /// <summary>
        /// Sistemde bulunan Controller ve Action sınıflarını veri tabanına kayıt işlemini gerçekleştirir.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="actionList"></param>
        /// <returns></returns>
        public bool CreateControllerAction(SystemControllers controller, List<SystemActions> actionList)
        {
            #region Add new Controller --> DB

            if (controller != null && controller.Name!="ErrorController")
            {
                
                List<SystemActions> actionEntity = new List<SystemActions>();

                int controllerId = FindController(controller.Name); // Veri tabanında sorgu!

                if (controllerId == 0)  // Veri Tabanında Yoksa Ekleme işlemi
                {
                    // Add System Controller
                    controller.Status = 0;
                    controller.CreateDate = DateTime.Now;
                    controller.Channel = System.Configuration.ConfigurationManager.AppSettings["Channel"];
                    _unitofwork.GetRepository<SystemControllers>().Add(controller);
                    _unitofwork.Commit();
                    
                    //Add System Actions
                    controllerId = controller.Id;
                }
                foreach (var item in actionList)
                {

                    var addAction =
                        _unitofwork.GetRepository<SystemActions>().Find(x => x.Name == item.Name && x.ControllerId == controllerId);

                    if (addAction==null)
                    {
                        SystemActions temp = new SystemActions()
                        {
                            Name = item.Name,
                            ControllerId = controllerId,
                            Status = 0,
                            CreateDate = DateTime.Now

                        };

                        actionEntity.Add(temp);
                    }
                }

                if (actionList.Count > 0)
                {
                    _unitofwork.GetRepository<SystemActions>().SaveAll(actionEntity);
                }



                return true;
            }
            else
            {
                return false;
            }


            #endregion

        }

        /// <summary>
        /// Controller ismini sorgulayıp Id Değerini döndürür.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int FindController(string Name)
        {
            if (!String.IsNullOrEmpty(Name))
            {                
                var controller = _unitofwork.GetRepository<SystemControllers>().Find(x => x.Name == Name);
                if (controller != null)
                {
                    return controller.Id;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Tüm Sayfa controller isim listesini getirir.
        /// </summary>
        /// <returns></returns>
        public List<Data.Models.SystemControllers> GetAllControllers()
        {            
            var result = _unitofwork.GetRepository<SystemControllers>().GetAll();
            return result.ToList();
        }


        public int FindAction(string Name)
        {
            if (!String.IsNullOrEmpty(Name))
            {                
                return _unitofwork.GetRepository<SystemActions>().Find(x => x.Name == Name).Id;
            }
            else
            {
                return 0;
            }
        }


        public void ClearControllerActions(Data.Models.SystemControllers newControllers, List<Data.Models.SystemActions> newActions)
        {
            // First Step Is controller in DB 

            
            var dbController = _unitofwork.GetRepository<SystemControllers>().Find(x => x.Name.Contains(newControllers.Name));            
            if (dbController != null)
            {

                var deletedActions = _unitofwork.GetRepository<SystemActions>().FindList(x => x.ControllerId == dbController.Id).Select(n => n.Name).Except(newActions.Select(e => e.Name));

                foreach (var deleteItem in deletedActions)
                {

                    var action =
                        _unitofwork.GetRepository<SystemActions>().Find(x => x.ControllerId == dbController.Id && x.Name == deleteItem);

                    // Delete RoleAction Table
                    var roleActions = _unitofwork.GetRepository<PermissionDetails>().FindList(x => x.ActionId == action.Id);
                    foreach (var roleactionItem in roleActions)
                    {
                        _unitofwork.GetRepository<PermissionDetails>().Delete(roleactionItem.Id);
                        _unitofwork.Commit();
                    }


                    // Delete Action Table
                    _unitofwork.GetRepository<SystemActions>().Delete(action.Id);
                    _unitofwork.Commit();


                }
            
            }

            
        }

        
        public List<Data.Models.PermissionDetails> GetSelectedPermissionDetails(int? permissonId)
        {
            var result = _unitofwork.GetRepository<PermissionDetails>().FindList(x => x.PermissionId == permissonId);
            return result.ToList();
        }
    }
}