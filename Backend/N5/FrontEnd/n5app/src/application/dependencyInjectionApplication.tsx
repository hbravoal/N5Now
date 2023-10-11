import {container} from 'tsyringe';
import { ICreateUserPermissionApplicationType } from '../domain/types/IHomeType';
import { CreateUserPermissionApplication } from './Home/CreateUserPermissionApplication';

export const DependencyInjectionApplication = (): void => {
  container.register(ICreateUserPermissionApplicationType, {
    useClass: CreateUserPermissionApplication,
  });
};


export default DependencyInjectionApplication;
