using AutoMapper;
using MegProject.Data;
using MegProject.Dto.CustomDto.ViewModels;

namespace MegProject.Dto
{
    public static class DtoMap
    {
        public static IMapper Map()
        {
            #region Test

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, DtoUsers>();
                cfg.CreateMap<DtoUsers, Users>();

                cfg.CreateMap<UserProfile, DtoUserProfile>();
                cfg.CreateMap<DtoUserProfile, UserProfile>();

                cfg.CreateMap<Roles, DtoRoles>();
                cfg.CreateMap<DtoRoles, Roles>();

                cfg.CreateMap<UserGroups, DtoUserGroups>();
                cfg.CreateMap<DtoUserGroups, UserGroups>();

                cfg.CreateMap<UserRoles, DtoUserRoles>();
                cfg.CreateMap<DtoUserRoles, UserRoles>();

                cfg.CreateMap<SystemActions, DtoSystemActions>();
                cfg.CreateMap<DtoSystemActions, SystemActions>();

                cfg.CreateMap<SystemControllers, DtoSystemControllers>();
                cfg.CreateMap<DtoSystemControllers, SystemControllers>();

                cfg.CreateMap<RoleAction, DtoRoleAction>();
                cfg.CreateMap<DtoRoleAction, RoleAction>();
                #region ViewModel Mapper

                cfg.CreateMap<UserGroupViewModel, DtoUserGroups>();
                cfg.CreateMap<RegisterViewModel, DtoUsers>();

                #endregion
            });

            return config.CreateMapper();


            #endregion

            //Mapper.CreateMap<Users, DtoUsers>();
            //Mapper.CreateMap<DtoUsers, Users>();

            //Mapper.CreateMap<UserProfile, DtoUserProfile>();
            //Mapper.CreateMap<DtoUserProfile, UserProfile>();

            //Mapper.CreateMap<Roles, DtoRoles>();
            //Mapper.CreateMap<DtoRoles, Roles>();

            //Mapper.CreateMap<UserGroups, DtoUserGroups>();
            //Mapper.CreateMap<DtoUserGroups, UserGroups>();

            //Mapper.CreateMap<UserRoles, DtoUserRoles>();
            //Mapper.CreateMap<DtoUserRoles, UserRoles>();

            //Mapper.CreateMap<SystemActions, DtoSystemActions>();
            //Mapper.CreateMap<DtoSystemActions, SystemActions>();

            //Mapper.CreateMap<SystemControllers, DtoSystemControllers>();
            //Mapper.CreateMap<DtoSystemControllers, SystemControllers>();

            //Mapper.CreateMap<RoleAction, DtoRoleAction>();
            //Mapper.CreateMap<DtoRoleAction, RoleAction>();

        }
    }
}