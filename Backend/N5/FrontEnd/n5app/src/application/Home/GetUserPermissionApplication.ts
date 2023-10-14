import 'reflect-metadata';
import {container} from 'tsyringe';
import { GetUserPermissionDto } from '../../domain/home/dtos/GetUserPermissionDto';
import GetUserPermissionResponseModel from '../../domain/home/model/GetUserPermissionResponseModel';
import IGetUserPermissionApplication from '../../domain/interfaces/application/IGetUserPermissionApplication';
import { IGetUserPermissionInfrastructure } from '../../domain/interfaces/infrastructure/IGetUserPermissionInfrastructure';
import { IGetUserPermissionInfrastructureType } from '../../domain/types/IHomeType';

const { v4: uuidv4 } = require('uuid'); 
  
// Generate a random UUID 
const random_uuid = uuidv4(); 

export class GetUserPermissionApplication implements IGetUserPermissionApplication {
  private readonly _infrastructure: IGetUserPermissionInfrastructure;

  constructor() {
    this._infrastructure = container.resolve<IGetUserPermissionInfrastructure>(
      IGetUserPermissionInfrastructureType,
    );

  }

  public async handler(page:number,pageSize:number): Promise<GetUserPermissionResponseModel> {
    let response = {} as GetUserPermissionResponseModel;
    try {
      const responseEP=  await this._infrastructure.Execute({Page:page,PageSize:pageSize,IdSession:random_uuid} as GetUserPermissionDto);
      if(responseEP && !responseEP.error){
        console.log('this.')
        response = responseEP;
      }else{
        //Change error if apply (Determine by Acceptace criterials).
      }
    } catch (error) {
      console.log('error',error)
      throw error
    }
    return response;
  }
}
