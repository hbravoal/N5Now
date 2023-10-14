import { GetUserPermissionDto } from "../../../domain/home/dtos/GetUserPermissionDto";
import { GetUserPermissionResponseDto } from "../../../domain/home/dtos/GetUserPermissionResponseDto";
import { USER_CONFIGURATION_PROXY_BASE,  USER_CONFIGURATION_PROXY_GET_PERMISSIONS } from "../../../domain/home/types/UserPermissionTypes";
import { IGetUserPermissionInfrastructure } from "../../../domain/interfaces/infrastructure/IGetUserPermissionInfrastructure";
import BaseAPI from "../../BaseAPI";

export default class GetUserPermissionInfrastructure
  extends BaseAPI
  implements IGetUserPermissionInfrastructure
{
  public async Execute(_request: GetUserPermissionDto): Promise<GetUserPermissionResponseDto> {

    return await this.post<any>(
      `${USER_CONFIGURATION_PROXY_GET_PERMISSIONS}`,
      _request,
    );
  }
}
