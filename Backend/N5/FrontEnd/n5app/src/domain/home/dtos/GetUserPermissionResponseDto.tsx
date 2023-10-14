export interface GetUserPermissionResponseDto {
    Error:string
    ErrorCode:number
    IdSession:string
    Permissions: UserPermissionResponseDto[]
}

export interface UserPermissionResponseDto{
    Id:number

    EmployeeForename:string
    EmployeeSurname:string
    PermissionTypeId:number
    PermissionDate:string
}