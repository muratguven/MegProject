using System;
using System.Collections.Generic;
using AutoMapper;
using MegProject.Data;
using MegProject.Data.Repositories.UserGroup;
using MegProject.Dto;
using MegProject.Business.Core;
using MegProject.Common;
using MegProject.Data.Core;
using Ninject;

namespace MegProject.Business.Manager.UserGroupAppService
{
    public class UserGroupApp:ApplicationCore,IUserGroupApp
    {

        private readonly IUserGroupRepository _userGroupRepository;

        [Inject]
        public IUnitOfWork _unitOfWork { private get; set; }

        //Constructor
        public UserGroupApp(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        /// <summary>
        /// Kullanıcı Grup listesini getirir.
        /// </summary>
        /// <returns></returns>
        public List<DtoUserGroups> GetAllUserGroups()
        {
            var result = _userGroupRepository.GetAll();
            return Mapper.Map<List<DtoUserGroups>>(result);
        }

        /// <summary>
        /// Kullanıcı Grubunu Kayıt İşlemini Gerçekleştirir.
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        public bool CreateOrUpdateUserGroup(DtoUserGroups userGroup)
        {
            try
            {
                if (userGroup == null)
                {                    
                    log.Error("DtoUsergroup bilgisi boş...");
                    return false;

                }

                #region Update

                if (userGroup.Id > 0)
                {

                    var modifyData = _userGroupRepository.Find(x => x.Id == userGroup.Id);

                    modifyData.UserGroupName = userGroup.UserGroupName;
                    modifyData.Status = userGroup.Status;
                    modifyData.ModifyDate = DateTime.Now;
                    
                    _userGroupRepository.UpdateAsync(modifyData);                    
                    log.Info("Usergroup güncellendi.");
                    return true;
                }
                #endregion


                #region Create
                var usergroup = Mapper.Map<UserGroups>(userGroup);

                usergroup.CreateDate = DateTime.Now;        
                _userGroupRepository.AddAsync(usergroup);
               // _userGroupRepository.Save();
                log.Info("Yeni UserGroup Oluşturuldu.");
                return true;
                #endregion

            }
            catch (Exception ex)
            {
                log.Fatal("Usergroup güncelleme ve eklemede hata oluştu.",ex);
                return false;
            }
        }

        /// <summary>
        /// Kullanıcı Grubunu Id değerine göre getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        
        public DtoUserGroups GetUserGroup(int Id)
        {
            if (Id != default(int))
            {
                var result = _userGroupRepository.Find(x => x.Id == Id);
                return Mapper.Map<DtoUserGroups>(result);

            }
            else
            {
                return null;
            }
        }


        public bool DeleteUserGroup(int Id)
        {
            try
            {
                if (Id != default(int) && Id > 0)
                {
                    var deleteUserGroup = _userGroupRepository.Find(x => x.Id == Id);
                    if (deleteUserGroup == null)
                    {
                        return false;
                    }

                    deleteUserGroup.Status = -1;
                    _userGroupRepository.Update(deleteUserGroup);
                    _userGroupRepository.Save();
                    log.Info(Id.CString()+" id UserGroup Silindi.");
                    return true;

                }
                else
                {
                    log.Error("UserGroup Id boş!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal("UserGroup silmede hata oluştu.",ex);
                return false;
            }
        }
    }
}