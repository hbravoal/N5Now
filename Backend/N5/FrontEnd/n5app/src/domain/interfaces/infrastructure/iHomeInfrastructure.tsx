import RequestHome from '../../home/model/requestHome';
import { TResponse } from '../../home/model/tResponse';
export interface IHomeInfrastructure {
  getHomeServer: (request: RequestHome) => Promise<TResponse<any[]>>;
}
