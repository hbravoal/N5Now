using AutoMapper;
using N5.User.Domain.DTO;
using N5.User.Domain.Entities;

namespace N5.User.Application.Configurations.Mappers;

public class UserPermissionMapper : Profile
{
    public UserPermissionMapper()
    {
        CreateMap<CreatePermissionDto, UserPermission>();

        CreateMap<UserPermission, CreatePermissionDto>();
    }
}