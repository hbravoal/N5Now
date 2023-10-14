export default interface GetUserPermissionResponseModel {
  error:string
  errorCode:number
  idSession:string
  permissions: UserPermissionResponseModel[]
}



export interface UserPermissionResponseModel{
  id:number
  employeeForename:string
  employeeSurname:string
  permissionTypeId:number
  permissionDate:string
}