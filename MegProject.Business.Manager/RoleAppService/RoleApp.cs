using System;
using AutoMapper;
using MegProject.Business.Core;
using MegProject.Data.Repositories.RoleAction;
using MegProject.Data.Repositories.Roles;
using MegProject.Dto;
using System.Collections.Generic;
using System.Linq;
using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Business.Manager.UserAppService;
using MegProject.Common;
using MegProject.Data.Repositories.Users;
using Ninject;

namespace MegProject.Business.Manager.RoleAppService
{
    public class RoleApp : ApplicationCore, IRoleApp
    {
        [Inject]
        public IRolesRepository _rolesRepository { set; private get; }
        [Inject]
        public IRoleActionRepository _roleActionRepository { set; private get; }
        [Inject]
        public IUserRepository _userRepository { set; private get; }

        #region Other Apps
        [Inject]
        public IUserApp _userApp { set; private get; }
        [Inject]
        public IControllerActionApp _controllerActionApp { set; private get; }

        #endregion


        public RoleApp()
        {

        }

        public List<DtoRoles> GetAllRoles()
        {
            var result = _rolesRepository.FindList(x=>x.Status!=-1);
            return Mapper.Map<List<DtoRoles>>(result);
        }

        public DtoRoles GetRole(int? Id)
        {
            var result = _rolesRepository.Find(x => x.Id == Id);
            return Mapper.Map<DtoRoles>(result);
        }

        public bool CreateOrUpdateRole(DtoRoles role)
        {
            if (role != null)
            {
                ///map
                var entity = Mapper.Map<Data.Roles>(role);

                if (role.Id != default(int))
                {

                    //Update New Role
                    try
                    {
                        _rolesRepository.Update(entity);
                        _rolesRepository.Save();
                        log.Info("Role güncellendi.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        log.Fatal("Role güncellemede hata oluştu.",ex);
                        return false;
                    }

                }
                else
                {

                    //Create New Role
                    try
                    {
                        _rolesRepository.Add(entity);
                        _rolesRepository.Save();
                        log.Info("Yeni rol oluştutuldu.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        log.Fatal("Yeni rol oluşturmada hata oluştu.",ex);
                        return false;
                    }


                }


            }
            else
            {
                log.Error("Role bilgisi boş gönderildi!");
                return false;
            }
        }


        public bool CreateOrUpdateRole(DtoRoles role, List<DtoRoleAction> actions)
        {
            if (role != null)
            {
                try
                {
                    if (role.Id != default(int))
                    {
                        //UPDATE ROLE ACTIONS***
                        // First Step Update Role data
                        var entityRole = _rolesRepository.Find(x => x.Id == role.Id);
                        entityRole.RoleName = role.RoleName;
                        entityRole.ModifyDate = DateTime.Now;
                        _rolesRepository.Update(entityRole);
                        _rolesRepository.Save();

                        //Action Add and Remove 

                        foreach (var actionItem in actions)
                        {


                            var dbRoleAction = _roleActionRepository.Find(
                                x => x.RoleId == role.Id &&
                                     x.ControllerId == actionItem.ControllerId &&
                                     x.ActionId == actionItem.ActionId);
                            if (dbRoleAction == null)
                            {
                                // Add new RoleAction

                                var entityRoleAction = Mapper.Map<Data.RoleAction>(actionItem);
                                entityRoleAction.RoleId = role.Id;
                                _roleActionRepository.Add(entityRoleAction);
                                _roleActionRepository.Save();
                            }


                        }

                        // Delete Role Action not in new roleactions

                        var deleteRoleActionList =
                            _roleActionRepository.FindList(x => x.RoleId == role.Id)
                                .Select(n => n.ActionId)
                                .Except(actions.Select(n => n.ActionId)).ToList();

                        foreach (var deleteItem in deleteRoleActionList)
                        {
                            var result = _roleActionRepository.Find(x => x.ActionId == deleteItem && x.RoleId == role.Id);
                            _roleActionRepository.Delete(result.Id);
                            _roleActionRepository.Save();
                        }

                        log.Info("Role ve action güncellendi.");

                        return true;

                    }
                    else
                    {
                        // Create Role 
                        var roleEntity = Mapper.Map<Data.Roles>(role);
                        roleEntity.CreateDate = DateTime.Now;
                        _rolesRepository.Add(roleEntity);
                        _rolesRepository.Save();
                        if (actions != null)
                        {
                            //Create Role Actions
                            var roleActionEntity = Mapper.Map<List<Data.RoleAction>>(actions);
                            foreach (var item in roleActionEntity)
                            {
                                item.RoleId = roleEntity.Id;
                                item.CreateDate = DateTime.Now;
                            }
                            _roleActionRepository.SaveAll(roleActionEntity);
                        }

                        log.Info("Yeni rol oluşturuldu.");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal("Rol ekleme ve güncellemede hata oluştu!",ex);
                    return false;
                }

            }
            else
            {
                log.Error("Role bilgisi boş gönderilmiş!");
                return false;
            }

        }

        public bool IsInRole(string role, int userId)
        {
            var roles = _rolesRepository.Find(x => x.RoleName.Contains(role)&&x.Status!=-1);
            if (roles == null)
            {
                return false;
            }
            else
            {

                var user = _userRepository.Find(x => x.Id == userId);

                if (user == null)
                {
                    return false;
                }

                var userRole = user.UserRoles.FirstOrDefault(x => x.RoleId == roles.Id);
                if (userRole == null)
                {
                    return false;
                }

                return true;
            }
        }

        //Yanlış kabul etmeyen method :P
        public bool IsInControlActions(int userId, string controllerName, string actionName)
        {
            var userRoles = _userApp.GetUserRole(userId);
            #region Validation
            if (userRoles.Count == 0)
            {
                return false;
            }

            if (String.IsNullOrEmpty(controllerName))
            {
                return false;
            }

            if (String.IsNullOrEmpty(actionName))
            {
                return false;
            }

            #endregion

            int controllerId = _controllerActionApp.FindController(controllerName);
            if (controllerId == 0)
            {
                return false; // Db de böyle bir controller yok!!!!
            }

            int actionId = _controllerActionApp.FindAction(actionName);
            if (actionId == 0)
            {
                return false; // Db de böyle bir action yok!!!!
            }

            bool result = false;

            foreach (var role in userRoles)
            {

                var roleAction = GetRoleAction(role.RoleId);

                if (roleAction.Any(x => x.ControllerId == controllerId))
                {
                    if (roleAction.Any(x => x.ActionId == actionId))
                    {
                        result = true;
                    }

                }

            }

            return result;
        }


        public List<DtoRoleAction> GetRoleAction(int roleId)
        {
            var result = _roleActionRepository.FindList(x => x.RoleId == roleId);

            return Mapper.Map<List<DtoRoleAction>>(result);
        }


        public bool DeleteRole(int? Id)
        {
            try
            {
                var role = _rolesRepository.Find(x => x.Id == Id);
                role.Status = -1;
                _rolesRepository.Update(role);
                _rolesRepository.Save();
                log.Info(Id.CString()+" Id'li rol silindi.");
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(Id.CString()+ " Id'li Rol silinirken hata oluştu.",ex);
                return false;
            }
        }
    }
}