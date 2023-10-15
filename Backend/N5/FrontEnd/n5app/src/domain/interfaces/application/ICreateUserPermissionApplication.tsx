import { CreateUserPermissionModel } from "../../home/model/CreateUserPermissionModel";

export default interface ICreateUserPermissionApplication {
  handler: (request: CreateUserPermissionModel) => Promise<any>;
}
