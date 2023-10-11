import { CreateUserPermissionDto } from "../../home/dtos/CreateUserPermissionDto";
import { CreateUserPermissionResponseDto } from "../../home/dtos/CreateUserPermissionResponseDto";

export interface ICreateUserPermissionInfrastructure {
  Execute: (request: CreateUserPermissionDto) => Promise<CreateUserPermissionResponseDto>;
}
