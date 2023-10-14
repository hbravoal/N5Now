export interface GetUserPermissionResponseDto {
    error:string
    errorCode:number
    idSession:string
    permissions: UserPermissionResponseDto[]
}

export interface UserPermissionResponseDto{
    id:number
    employeeForename:string
    employeeSurname:string
    permissionTypeId:number
    permissionDate:string
}