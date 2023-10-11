import RequestHome from "../../home/model/requestHome";

export default interface ICreateUserPermissionApplication {
  handler: (request: RequestHome) => Promise<any>;
}
