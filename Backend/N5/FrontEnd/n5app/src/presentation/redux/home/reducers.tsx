import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import ResponseHome from '../../../domain/home/model/responseHome';
import HomeState from '../../../domain/home/state/HomeState';

export const homeSlice = createSlice({
  name: 'home',
  initialState: HomeState,
  reducers: {
    homePageBegin: state => {
      state.loading = true;
    },
    homePageSuccess: (state, response: PayloadAction<ResponseHome>) => {
      state.data = response.payload;
      state.loading = false;
    },
  },
});

export const {homePageBegin, homePageSuccess} = homeSlice.actions;
export default homeSlice.reducer;
