﻿using System;
using System.Data.Entity;
using System.Transactions;
using MegProject.Data.Repositories.Roles;
using MegProject.Data.Repositories.SystemActions;
using MegProject.Data.Repositories.SystemControllers;
using MegProject.Data.Repositories.UserGroup;
using MegProject.Data.Repositories.UserProfile;
using MegProject.Data.Repositories.UserRoles;
using MegProject.Data.Repositories.Users;

namespace MegProject.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public DbContext Context
        {
            get
            {
                return _context;
            }
        }

        public UnitOfWork()
        {
            _context = new MegProjectDbEntities();
        }

        public UnitOfWork(DbContext context)
        {
            _context = context;

        }


        public IUserRepository UserRepository { get; private set; }
        public IUserRolesRepository UserRolesRepository { get; private set; }
        public IUserProfileRepository UserProfileRepository { get; private set; }
        public IUserGroupRepository UserGroupRepository { get; private set; }
        public ISystemControllerRepository SystemControllerRepository { get; private set; }
        public ISystemActionRepository SystemActionRepository { get; private set; }
        public IRolesRepository RolesRepository { get; private set; }
        

      

        public int Commit()
        {
            using (TransactionScope tcx = new TransactionScope())
            {
               return  Context.SaveChanges();
            }
        }

        #region Disposing

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose(); 
            }
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}