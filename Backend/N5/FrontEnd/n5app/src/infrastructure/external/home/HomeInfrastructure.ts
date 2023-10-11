import RequestHome from '../../../domain/home/model/requestHome';
import { TResponse } from '../../../domain/home/model/tResponse';
import {IHomeInfrastructure} from '../../../domain/interfaces/infrastructure/iHomeInfrastructure';
import BaseAPI from '../../BaseAPI';
export default class HomeInfrastructure
  extends BaseAPI
  implements IHomeInfrastructure
{
  public async getHomeServer(_request: RequestHome): Promise<TResponse<any[]>> {

    const responseServer = await this.get(
      'https://URL.com',
    );
    return responseServer.data;

  }
}
