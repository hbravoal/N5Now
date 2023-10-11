import {configureStore} from '@reduxjs/toolkit';
import {rootReducer} from './rootReducers';
import createSagaMiddleware from 'redux-saga';
import logger from 'redux-logger';
import rootSaga from './rootSagas';

const sagaMiddleware = createSagaMiddleware();
const middleware = [];
middleware.push(sagaMiddleware);
middleware.push(logger);

export const store = configureStore({
  reducer: rootReducer,
  middleware: middleware,
});

sagaMiddleware.run(rootSaga);
