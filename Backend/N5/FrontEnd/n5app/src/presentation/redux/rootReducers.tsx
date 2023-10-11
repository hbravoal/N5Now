import {combineReducers} from '@reduxjs/toolkit';
import { IState } from '../../domain/interfaces/presentation/IState';
import homeReducer from './home/reducers';

export const rootReducer = combineReducers<IState>({
  home: homeReducer,
});
