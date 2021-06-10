import { combineReducers } from "redux";
import messageReducer from './messageReducer';
import loadingReducer from './loadingReducer';
import homeReducer from '../reducers/homeReducer';
import loginReducer from '../reducers/loginReducer';
import usuarioReducer from '../reducers/usuarioReducers';
import clienteReducers from '../reducers/clienteReducers';

const rootReducer = combineReducers({  
     load:loginReducer,
     message:messageReducer,
     requestloading:loadingReducer,
     home:homeReducer,
     usuario:usuarioReducer, 
     cliente:clienteReducers,
});

export default rootReducer;
