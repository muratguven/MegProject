using System;
using AutoMapper;
using MegProject.Business.Core;
using MegProject.Data.Repositories.Roles;
using MegProject.Dto;
using System.Collections.Generic;
using System.Linq;
using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Business.Manager.UserAppService;
using MegProject.Common;
using MegProject.Data.Repositories.PermissionDetails;
using MegProject.Data.Repositories.Permissions;
using MegProject.Data.Repositories.RolePermissions;
using MegProject.Data.Repositories.Users;
using Ninject;

namespace MegProject.Business.Manager.RoleAppService
{
    public class RoleApp : ApplicationCore, IRoleApp
    {
        [Inject]
        public IRolesRepository _rolesRepository { set; private get; }
       
        [Inject]
        public IUserRepository _userRepository { set; private get; }

        [Inject]
        public IRolePermissionsRepository _rolePermissionRepository { set; private get; }

        [Inject]
        public IPermissionsRepository _permissionRepository { set; private get; }

        [Inject]
        public IPermissionDetailsRepository _permissionDetailRepository { set; private get; }

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


        public bool CreateOrUpdateRole(DtoRoles role, List<DtoRolePermissions> permissions)
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
                        _rolesRepository.UpdateAsync(entityRole);
                        

                        //Action Add and Remove BURADA KALDIK

                        foreach (var permissionItem in permissions)
                        {
                            
                            var dbRolePermission = _rolePermissionRepository.Find(
                                x => x.RoleId == role.Id && x.PermissionId==permissionItem.Id);
                            if (dbRolePermission == null)
                            {
                                // Add new RoleAction

                                var entityRolePermission = Mapper.Map<Data.RolePermissions>(permissionItem);
                                entityRolePermission.RoleId = role.Id;
                                _rolePermissionRepository.AddAsync(entityRolePermission);

                            }


                        }

                        // Delete Role Permissions not in new roleactions

                        var deleteRolePermissionList =
                            _rolePermissionRepository.FindList(x => x.RoleId == role.Id)
                                .Select(n => n.PermissionId)
                                .Except(permissions.Select(n => n.PermissionId)).ToList();

                        foreach (var deleteItem in deleteRolePermissionList)
                        {
                            var result = _rolePermissionRepository.Find(x => x.PermissionId == deleteItem && x.RoleId == role.Id);
                            _rolePermissionRepository.DeleteAsync(result);
                        }

                        log.Info("Role ve İzinler güncellendi.");

                        return true;

                    }
                    else
                    {
                        // Create Role 
                        var roleEntity = Mapper.Map<Data.Roles>(role);
                        roleEntity.CreateDate = DateTime.Now;
                        if (permissions != null)
                        {
                            var rolePermissionEntityList = Mapper.Map<ICollection<Data.RolePermissions>>(permissions);
                            foreach (var item in rolePermissionEntityList)
                            {                               
                               roleEntity.RolePermissions.Add(item);
                            }
                        }
                        
                        _rolesRepository.AddAsync(roleEntity);
                        
                        

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

        #region Permissions

        //Yanlış kabul etmeyen method :P
        public bool IsInPermissionDetail(int userId, string controllerName, string actionName)
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

                //var roleAction = GetRoleAction(role.RoleId);


                //if (roleAction.Any(x => x.ControllerId == controllerId))
                //{
                //    if (roleAction.Any(x => x.ActionId == actionId))
                //    {
                //        result = true;
                //    }

                //}

                if (role.Permission.PermissionDetails.Any(x => x.ControllerId == controllerId && x.ActionId == actionId))
                {
                    return true;
                }

            }

            return result;
        }

        public bool CreateOrUpdatePermission(DtoPermission permission)
        {
            try
            {
                if (permission != null)
                {
                    var entity = Mapper.Map<Data.Permission>(permission);
                    if (permission.Id != default(int))
                    {
                        #region Update

                        
                        _permissionRepository.UpdateAsync(entity);
                        return true;

                        #endregion

                    }
                    else
                    {
                        #region Create 

                        _permissionRepository.AddAsync(entity);
                        return true;
                        #endregion
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Bussiness hatası:"+ex);
                return false;
            }
        }

        public List<DtoRolePermissions> GetRolePermissions(int roleId)
        {
            var result = _rolePermissionRepository.FindList(x => x.RoleId == roleId);

            return Mapper.Map<List<DtoRolePermissions>>(result);
        }


        public bool CreateOrUpdatePermission(DtoPermission permission, List<DtoPermissionDetails> details)
        {
            if (permission != null)
            {
                try
                {
                    if (permission.Id != default(int))
                    {
                        //UPDATE ROLE ACTIONS***
                        // First Step Update Role data
                        var entityPermission = _permissionRepository.Find(x => x.Id == permission.Id);
                        entityPermission.PermissionName = permission.PermissionName;
                        entityPermission.ModifyDate = DateTime.Now;
                        _permissionRepository.UpdateAsync(entityPermission);

                        //(PermissionDetail) Add and Remove 

                        foreach (var permissionItem in details)
                        {

                            var dbRolePermission = _permissionDetailRepository.Find(
                                x => x.PermissionId == permission.Id &&
                                     x.ControllerId == permissionItem.ControllerId &&
                                     x.ActionId == permissionItem.ActionId);
                            if (dbRolePermission == null)
                            {
                                // Add new RoleAction

                                var entityPermissionDetail = Mapper.Map<Data.PermissionDetails>(permissionItem);
                                entityPermissionDetail.PermissionId = permission.Id;
                                _permissionDetailRepository.AddAsync(entityPermissionDetail);

                            }


                        }

                        // Delete Role Action not in new roleactions

                        var deletePermissionDetail =
                            _permissionDetailRepository.FindList(x => x.PermissionId == permission.Id)
                                .Select(n => n.ActionId)
                                .Except(details.Select(n => n.ActionId)).ToList();

                        foreach (var deleteItem in deletePermissionDetail)
                        {
                            var result = _permissionDetailRepository.Find(x => x.ActionId == deleteItem && x.PermissionId == permission.Id);
                            _permissionDetailRepository.DeleteAsync(result);
                        }

                        log.Info("İzin bilgisi güncellendi.");

                        return true;

                    }
                    else
                    {
                        // Create Permission 
                        var permissionEntity = Mapper.Map<Data.Permission>(permission);
                        permissionEntity.CreateDate = DateTime.Now;
                        _permissionRepository.AddAsync(permissionEntity);
                        if (details != null)
                        {
                            //Create Role Actions
                            var permissionDetailEntity = Mapper.Map<List<Data.PermissionDetails>>(details);
                            foreach (var item in permissionDetailEntity)
                            {
                                item.PermissionId = permissionEntity.Id;
                                item.CreateDate = DateTime.Now;
                            }
                            _permissionDetailRepository.BulkInsert(permissionDetailEntity);
                        }

                        log.Info("Yeni izin oluşturuldu.");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal("İzin ekleme ve güncellemede hata oluştu!", ex);
                    return false;
                }

            }
            else
            {
                log.Error("İzin bilgisi boş gönderilmiş!");
                return false;
            }
        }


        #endregion




    }
}