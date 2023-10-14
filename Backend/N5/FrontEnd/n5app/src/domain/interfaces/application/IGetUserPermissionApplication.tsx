import RequestHome from "../../home/model/requestHome";

export default interface IGetUserPermissionApplication {
  handler: (page:number,pageSize:number) => Promise<any>;
}
