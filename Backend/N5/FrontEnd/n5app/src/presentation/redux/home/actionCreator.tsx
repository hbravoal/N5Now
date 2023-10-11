import 'reflect-metadata';
import {container} from 'tsyringe';
import {call, put, takeLatest} from 'redux-saga/effects';
import { homePageSuccess } from './reducers';
import ICreateUserPermissionApplication from '../../../domain/interfaces/application/ICreateUserPermissionApplication';
import { ICreateUserPermissionInfrastructureType } from '../../../domain/types/IHomeType';

function* homePage(request:any): any {
  try {
    const homeApplication =
      container.resolve<ICreateUserPermissionApplication>(ICreateUserPermissionInfrastructureType);
    const response = yield call(
      async () => await homeApplication.handler(request),
    );
    yield put(homePageSuccess(response));
  } catch (ex) {
    console.error(ex);
  }
}

export default function* homeSaga(): any {
  yield takeLatest('home/homePageBegin', homePage);
}
