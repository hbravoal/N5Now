using AutoMapper;
using N5.User.Domain.DTO;
using N5.User.Domain.Entities;

namespace N5.User.Application.Configurations.Mappers;

public class UserPermissionMapper : Profile
{
    public UserPermissionMapper()
    {
        #region Create
        CreateMap<CreatePermissionDto, UserPermission>();

        CreateMap<UserPermission, CreatePermissionDto>();
        #endregion

        #region Modify
         CreateMap<ModifyPermissionDto, UserPermission>();

        CreateMap<UserPermission, ModifyPermissionDto>();
        #endregion

        #region Get
        CreateMap<PermissionDto, UserPermission>();

        CreateMap<UserPermission, PermissionDto>(); 
        #endregion




        //    CreateMap<PermissionDto, UserPermission>();
        //CreateMap<UserPermission, PermissionDto>();

        //CreateMap<List<PermissionDto>, List<UserPermission>>();
        //CreateMap<List<UserPermission>, List<PermissionDto>>();

    }
}