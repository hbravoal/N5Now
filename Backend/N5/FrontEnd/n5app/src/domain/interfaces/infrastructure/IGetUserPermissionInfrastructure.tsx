import { GetUserPermissionDto } from "../../home/dtos/GetUserPermissionDto";
import { GetUserPermissionResponseDto } from "../../home/dtos/GetUserPermissionResponseDto";

export interface IGetUserPermissionInfrastructure {
  Execute: (request: GetUserPermissionDto) => Promise<GetUserPermissionResponseDto>;
}
