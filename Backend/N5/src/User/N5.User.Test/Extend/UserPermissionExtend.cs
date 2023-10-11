using Bogus;
using N5.User.Domain.DTO;
using N5.User.Domain.Entities;

namespace N5.User.Test.Extend;

public static class UserPermissionExtend
{
    public static CreatePermissionDto GenerateObject(this Faker<CreatePermissionDto> faker)
    {
        faker.RuleFor(a => a.IdSession, z => Guid.NewGuid().ToString());
        faker.RuleFor(a => a.PermissionDate, z => DateTime.UtcNow);
        faker.RuleFor(a => a.EmployeeForename, z => z.Lorem.Lines(2));
        faker.RuleFor(a => a.EmployeeSurname, z => z.Name.FirstName());
        faker.RuleFor(a => a.PermissionTypeId, z => z.Random.Int(1, 2000));
        return faker.Generate();
    }

    public static CreatePermissionCompleteDTO GenerateObject(this Faker<CreatePermissionCompleteDTO> faker)
    {
        faker.RuleFor(a => a.IdSession, z => Guid.NewGuid().ToString());
        faker.RuleFor(a => a.Id, z => z.Random.Int(1, 99999));
        return faker.Generate();
    }

    public static UserPermission GenerateObject(this Faker<UserPermission> faker)
    {
        faker.RuleFor(a => a.PermissionDate, z => DateTime.UtcNow);
        faker.RuleFor(a => a.EmployeeForename, z => z.Lorem.Lines(2));
        faker.RuleFor(a => a.EmployeeSurname, z => z.Name.FirstName());
        faker.RuleFor(a => a.PermissionTypeId, z => z.Random.Int(1, 2000));
        faker.RuleFor(a => a.Enabled, z => true);
        faker.RuleFor(a => a.CreatedDate, z => DateTime.UtcNow);
        faker.RuleFor(a => a.LastUpdate, z => DateTime.UtcNow);
        return faker.Generate();
    }

    public static List<UserPermission> GenerateObjectlist(this Faker<UserPermission> faker)
    {
        faker.RuleFor(a => a.PermissionDate, z => DateTime.UtcNow);
        faker.RuleFor(a => a.EmployeeForename, z => z.Lorem.Lines(2));
        faker.RuleFor(a => a.EmployeeSurname, z => z.Name.FirstName());
        faker.RuleFor(a => a.PermissionTypeId, z => z.Random.Int(1, 2000));
        faker.RuleFor(a => a.Enabled, z => true);
        faker.RuleFor(a => a.CreatedDate, z => DateTime.UtcNow);
        faker.RuleFor(a => a.LastUpdate, z => DateTime.UtcNow);
        return faker.Generate(5).ToList();
    }



}