import React from 'react';
import {Provider} from 'react-redux';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import { store } from './presentation/redux/store';
import DependencyInjectionApplication from './application/dependencyInjectionApplication';
import DependencyInjectionInfrastructure from './infrastructure/dependencyInfrastructure';
import HomePage from './presentation/pages/Home/HomePage';
import { ThemeProvider } from '@material-tailwind/react';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
DependencyInjectionApplication();
DependencyInjectionInfrastructure();
root.render(
  <React.StrictMode>
    <Provider store={store}>
    <ThemeProvider>
      <HomePage />
    </ThemeProvider>

    </Provider>
  </React.StrictMode>
);
reportWebVitals();
