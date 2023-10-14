import 'reflect-metadata';
import {container} from 'tsyringe';
import { GetUserPermissionDto } from '../../domain/home/dtos/GetUserPermissionDto';
import IGetUserPermissionApplication from '../../domain/interfaces/application/IGetUserPermissionApplication';
import { IGetUserPermissionInfrastructure } from '../../domain/interfaces/infrastructure/IGetUserPermissionInfrastructure';
import { ICreateUserPermissionInfrastructureType, IGetUserPermissionInfrastructureType } from '../../domain/types/IHomeType';


export class GetUserPermissionApplication implements IGetUserPermissionApplication {
  private readonly _infrastructure: IGetUserPermissionInfrastructure;

  constructor() {
    this._infrastructure = container.resolve<IGetUserPermissionInfrastructure>(
      IGetUserPermissionInfrastructureType,
    );

  }

  public async handler(page:number,pageSize:number): Promise<any> {
    try {
      return  await this._infrastructure.Execute({Page:page,PageSize:pageSize} as GetUserPermissionDto);
    } catch (error) {
      throw Error()
    }

  }
}
