import 'reflect-metadata';
import {container} from 'tsyringe';
import { CreateUserPermissionDto } from '../../domain/home/dtos/CreateUserPermissionDto';
import RequestHome from '../../domain/home/model/requestHome';
import ICreateUserPermissionApplication from '../../domain/interfaces/application/ICreateUserPermissionApplication';
import { ICreateUserPermissionInfrastructure } from '../../domain/interfaces/infrastructure/ICreateUserPermissionInfrastructure';
import { ICreateUserPermissionInfrastructureType } from '../../domain/types/IHomeType';


/**
 * [Application] Generate code
 */
export class CreateUserPermissionApplication implements ICreateUserPermissionApplication {
  private readonly _infrastructure: ICreateUserPermissionInfrastructure;

  constructor() {
    this._infrastructure = container.resolve<ICreateUserPermissionInfrastructure>(
      ICreateUserPermissionInfrastructureType,
    );

  }

  public async handler(_request: RequestHome): Promise<any> {
    try {
      return  await this._infrastructure.Execute({} as CreateUserPermissionDto);
    } catch (error) {
      throw Error()
    }

  }
}
