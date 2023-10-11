import RequestHome from '../../home/model/requestHome';
import { TResponse } from '../../home/model/tResponse';
export default interface IHomeApplication {
  handler: (request: RequestHome) => Promise<TResponse<any[]>>;
}
