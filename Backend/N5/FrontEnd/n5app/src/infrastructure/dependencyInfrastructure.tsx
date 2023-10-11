import {container} from 'tsyringe';
import { IHomeInfrastructureType } from '../domain/types/IHomeType';
import HomeInfrastructure from './external/home/HomeInfrastructure';

export const DependencyInjectionInfrastructure = (): void => {
  container.register(IHomeInfrastructureType, {
    useClass: HomeInfrastructure,
  });
};
export default DependencyInjectionInfrastructure;
