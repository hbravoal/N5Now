import {container} from 'tsyringe';
import { ICreateUserPermissionInfrastructureType } from '../domain/types/IHomeType';
import CreateUserPermissionInfrastructure from './external/home/CreateUserPermissionInfrastructure';

export const DependencyInjectionInfrastructure = (): void => {
  container.register(ICreateUserPermissionInfrastructureType, {
    useClass: CreateUserPermissionInfrastructure,
  });
};
export default DependencyInjectionInfrastructure;
