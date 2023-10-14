import {container} from 'tsyringe';
import { ICreateUserPermissionInfrastructureType, IGetUserPermissionInfrastructureType } from '../domain/types/IHomeType';
import CreateUserPermissionInfrastructure from './external/home/CreateUserPermissionInfrastructure';
import GetUserPermissionInfrastructure from './external/home/GetUserPermissionInfrastructure';

export const DependencyInjectionInfrastructure = (): void => {
  container.register(ICreateUserPermissionInfrastructureType, {
    useClass: CreateUserPermissionInfrastructure,
  });
  container.register(IGetUserPermissionInfrastructureType, {
    useClass: GetUserPermissionInfrastructure,
  });
};
export default DependencyInjectionInfrastructure;
