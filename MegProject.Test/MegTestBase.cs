using System;
using System.Collections.Generic;
using System.Linq;
using MegProject.Business.Manager.App_Start;
using MegProject.Business.Manager.PermissionAppService;
using MegProject.Data.Models;
using Moq;
using Ninject;

namespace MegProject.Test
{
    public class MegTestBase
    {

        private IPermissionApp _permissionApp;

        public MegTestBase()
        {            
            _permissionApp = new PermissionApp();
        }
        

        #region Getters And Setters

        public PermissionDetails PermissionDetail
        {
            get
            {
                return new PermissionDetails()
                {
                    ActionId = 1,
                    ControllerId = 1,
                    Status = 0
                };
            }
        }


        public Permission Permission
        {
            get
            {
                List<PermissionDetails> permissionDetailList = new List<PermissionDetails>();
                permissionDetailList.Add(PermissionDetail);
                return new Permission()
                {

                    PermissionName = "Sistem Test Yöneticisi",
                    Status = 0,
                    PermissionDetails = permissionDetailList,
                    CreateDate = DateTime.Now
                };
            }
        }

        #endregion

        #region Permission App Test Methods

        public Permission CreatePermission()
        {
            
            return _permissionApp.Add<Permission>(Permission);
        }


        public Permission UpdatePermissions()
        {
            var permission = _permissionApp.Get<Permission>(x => x.Id == 1).FirstOrDefault();
            permission.PermissionName = "Updated Sistem Yönetcisi";

            var firstOrDefault = permission.PermissionDetails.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.Status = -1;
            
            return _permissionApp.Update<Permission>(permission);

        }
        #endregion




    }
}