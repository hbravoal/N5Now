import {container} from 'tsyringe';
import { ICreateUserPermissionApplicationType, IGetUserPermissionApplicationType } from '../domain/types/IHomeType';
import { CreateUserPermissionApplication } from './Home/CreateUserPermissionApplication';
import { GetUserPermissionApplication } from './Home/GetUserPermissionApplication';

export const DependencyInjectionApplication = (): void => {
  container.register(ICreateUserPermissionApplicationType, {
    useClass: CreateUserPermissionApplication,
  });
  container.register(IGetUserPermissionApplicationType, {
    useClass: GetUserPermissionApplication,
  });
};


export default DependencyInjectionApplication;
