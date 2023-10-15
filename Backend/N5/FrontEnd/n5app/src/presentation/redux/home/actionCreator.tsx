import 'reflect-metadata';
import {container} from 'tsyringe';
import {call, put, takeLatest} from 'redux-saga/effects';
import { homePageSuccess } from './reducers';
import ICreateUserPermissionApplication from '../../../domain/interfaces/application/ICreateUserPermissionApplication';
import {  ICreateUserPermissionApplicationType, IGetUserPermissionApplicationType, IGetUserPermissionInfrastructureType } from '../../../domain/types/IHomeType';
import IGetUserPermissionApplication from '../../../domain/interfaces/application/IGetUserPermissionApplication';
import GetUserPermissionResponseModel from '../../../domain/home/model/GetUserPermissionResponseModel';

function* homePage(request:any): any {
  try {
    const homeApplication =
      container.resolve<IGetUserPermissionApplication>(IGetUserPermissionApplicationType);
    const response :GetUserPermissionResponseModel = yield call(
      async () => await homeApplication.handler(request?.Page ??1 ,request.PageSize ?? 100),
    );
    console.log('resposne from pageget')
    if(response && !response.error)
     yield put(homePageSuccess(response.permissions));
  } catch (ex) {
    console.log('ex')
    console.error(ex);
  }
}
function* createPermission(request:any): any {
  try {

    const homeApplication =
      container.resolve<ICreateUserPermissionApplication>(ICreateUserPermissionApplicationType);
    const response = yield call(
      async () => await homeApplication.handler(request.payload),
    );
    console.log('response:::',response)
    // yield put(homePageSuccess(response));
  } catch (ex) {
    console.log('eerxception')
    console.error(ex);
  }
}

export default function* homeSaga(): any {
  yield takeLatest('home/homePageLoad', homePage);
  yield takeLatest('home/createPermission', createPermission);
}
