import { combineReducers } from "redux";
import messageReducer from './messageReducer';
import loadingReducer from './loadingReducer';
import homeReducer from '../reducers/homeReducer';
import loginReducer from '../reducers/loginReducer';
import usuarioReducer from '../reducers/usuarioReducers';
import clienteReducers from '../reducers/clienteReducers';
import cargarAplicacionesReducer from '../reducers/cargarAplicacionesReducer';

const rootReducer = combineReducers({  
     load:loginReducer,
     message:messageReducer,
     requestloading:loadingReducer,
     home:homeReducer,
     usuario:usuarioReducer, 
     cliente:clienteReducers,
     cargarAplicaciones:cargarAplicacionesReducer
});

export default rootReducer;
