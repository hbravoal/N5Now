import 'reflect-metadata';
import {container} from 'tsyringe';
import { CreateUserPermissionDto } from '../../domain/home/dtos/CreateUserPermissionDto';
import { CreateUserPermissionModel } from '../../domain/home/model/CreateUserPermissionModel';
import ICreateUserPermissionApplication from '../../domain/interfaces/application/ICreateUserPermissionApplication';
import { ICreateUserPermissionInfrastructure } from '../../domain/interfaces/infrastructure/ICreateUserPermissionInfrastructure';
import { ICreateUserPermissionInfrastructureType } from '../../domain/types/IHomeType';

const { v4: uuidv4 } = require('uuid'); 
  
// Generate a random UUID 
const random_uuid = uuidv4(); 

export class CreateUserPermissionApplication implements ICreateUserPermissionApplication {
  private readonly _infrastructure: ICreateUserPermissionInfrastructure;

  constructor() {
    this._infrastructure = container.resolve<ICreateUserPermissionInfrastructure>(
      ICreateUserPermissionInfrastructureType,
    );

  }
  

  public async handler(_request: CreateUserPermissionModel): Promise<any> {
    try {
      
      var isoDateString = new Date(_request.PermissionDate).toISOString();
      _request.IdSession=random_uuid;
      console.log('isoDateString',isoDateString)
      _request.PermissionDate = isoDateString;
      return  await this._infrastructure.Execute(_request as CreateUserPermissionDto);
    } catch (error) {
      console.log(error)
    }
  }
}
