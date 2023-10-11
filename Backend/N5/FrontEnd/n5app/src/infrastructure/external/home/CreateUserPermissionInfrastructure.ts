import { CreateUserPermissionDto } from "../../../domain/home/dtos/CreateUserPermissionDto";
import { CreateUserPermissionResponseDto } from "../../../domain/home/dtos/CreateUserPermissionResponseDto";
import { USER_CONFIGURATION_PROXY_BASE, USER_CONFIGURATION_PROXY_CREATE_PERMISSION } from "../../../domain/home/types/UserPermissionTypes";
import { ICreateUserPermissionInfrastructure } from "../../../domain/interfaces/infrastructure/ICreateUserPermissionInfrastructure";
import BaseAPI from "../../BaseAPI";

export default class CreateUserPermissionInfrastructure
  extends BaseAPI
  implements ICreateUserPermissionInfrastructure
{
  public async Execute(_request: CreateUserPermissionDto): Promise<CreateUserPermissionResponseDto> {

    return await this.post<any>(
      `/V${USER_CONFIGURATION_PROXY_BASE}${USER_CONFIGURATION_PROXY_CREATE_PERMISSION}`,
      _request,
    );
  }
}
