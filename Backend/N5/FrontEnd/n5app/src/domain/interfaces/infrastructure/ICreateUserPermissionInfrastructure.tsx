import { CreateUserPermissionDto } from 'domain/home/dtos/CreateUserPermissionDto';
import { CreateUserPermissionResponseDto } from 'domain/home/dtos/CreateUserPermissionResponseDto';

export interface ICreateUserPermissionInfrastructure {
  Execute: (request: CreateUserPermissionDto) => Promise<CreateUserPermissionResponseDto>;
}
