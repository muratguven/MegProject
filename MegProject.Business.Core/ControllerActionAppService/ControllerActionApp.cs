using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MegProject.Business.Core;
using MegProject.Data;
using MegProject.Data.Core;
using MegProject.Data.Core.Base;
using MegProject.Data.Models;
using MegProject.Dto;
using Ninject;

namespace MegProject.Business.Core.ControllerActionAppService
{
    public class ControllerActionApp:ApplicationCore,IControllerActionApp
    {

        [Inject]
        public IUnitOfWork _unitofwork { private get; set; }
        [Inject] 
        public IGenericRepository<SystemControllers> _systemControllerRepository { private get; set; }
        [Inject]
        public IGenericRepository<SystemActions> _systemActionRepository { private get; set; }
        [Inject]
        public IGenericRepository<PermissionDetails> _permissionDetailsRepository { private get; set; }


        public ControllerActionApp()
        {
            _systemControllerRepository = _unitofwork.GetRepository<SystemControllers>();
            _systemActionRepository = _unitofwork.GetRepository<SystemActions>();
            _permissionDetailsRepository = _unitofwork.GetRepository<PermissionDetails>();
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

                var controllerEntity = Mapper.Map<SystemControllers>(controller);
                List<SystemActions> actionEntity = new List<SystemActions>();

                int controllerId = FindController(controller.Name); // Veri tabanında sorgu!

                if (controllerId == 0)  // Veri Tabanında Yoksa Ekleme işlemi
                {
                    // Add System Controller
                    controllerEntity.Status = 0;
                    controllerEntity.CreateDate = DateTime.Now;
                    _systemControllerRepository.Add(controllerEntity);
                    _systemControllerRepository.Save();

                    //Add System Actions
                    controllerId = controllerEntity.Id;
                }
                foreach (var item in actionList)
                {

                    var addAction =
                        _systemActionRepository.Find(x => x.Name == item.Name && x.ControllerId == controllerId);

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
                    _systemActionRepository.SaveAll(actionEntity);
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
                var controller = _systemControllerRepository.Find(x => x.Name == Name);
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
            var result = _systemControllerRepository.GetAll();
            return result.ToList();
        }


        public int FindAction(string Name)
        {
            if (!String.IsNullOrEmpty(Name))
            {
                return _systemActionRepository.Find(x => x.Name == Name).Id;
            }
            else
            {
                return 0;
            }
        }


        public void ClearControllerActions(Data.Models.SystemControllers newControllers, List<Data.Models.SystemActions> newActions)
        {
            // First Step Is controller in DB 
            
            
            var dbController = _systemControllerRepository.Find(x => x.Name.Contains(newControllers.Name));
            
            var entityAction = Mapper.Map<List<Data.Models.SystemActions>>(newActions);

            if (dbController != null)
            {
                var deletedActions = _systemActionRepository.FindList(x => x.ControllerId == dbController.Id).Select(n => n.Name).Except(entityAction.Select(e => e.Name));

                foreach (var deleteItem in deletedActions)
                {

                    var action =
                        _systemActionRepository.Find(x => x.ControllerId == dbController.Id && x.Name == deleteItem);

                    // Delete RoleAction Table
                    var roleActions = _permissionDetailsRepository.FindList(x => x.ActionId == action.Id);
                    foreach (var roleactionItem in roleActions)
                    {
                        _permissionDetailsRepository.Delete(roleactionItem.Id);
                        _permissionDetailsRepository.Save();
                    }


                    // Delete Action Table
                    _systemActionRepository.Delete(action.Id);
                    _systemActionRepository.Save();


                }
            
            }

            
        }



        public List<Data.Models.PermissionDetails> GetSelectedPermissionDetails(int? permissonId)
        {
            var result = _permissionDetailsRepository.FindList(x => x.PermissionId == permissonId);
            return result.ToList();
        }
    }
}