import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import ResponseHome, { UserPermissionResponseModel } from '../../../domain/home/model/GetUserPermissionResponseModel';
import HomeState from '../../../domain/home/state/HomeState';

export const homeSlice = createSlice({
  name: 'home',
  initialState: HomeState,
  reducers: {
    homePageBegin: state => {
      state.loading = true;
    },
    homePageEnd: state => {
      state.loading = false;
    },
    homePageSuccess: (state, response: PayloadAction<UserPermissionResponseModel[]>) => {
      state.data = response.payload;
    },
  },
});

export const {homePageBegin,homePageEnd, homePageSuccess} = homeSlice.actions;
export default homeSlice.reducer;
