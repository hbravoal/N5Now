import GetUserPermissionResponseModel from "../../home/model/GetUserPermissionResponseModel";
export default interface IGetUserPermissionApplication {
  handler: (page:number,pageSize:number) => Promise<GetUserPermissionResponseModel>;
}
