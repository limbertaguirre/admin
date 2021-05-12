import { combineReducers } from "redux";
import messageReducer from './messageReducer';
import loadingReducer from './loadingReducer';
import homeReducer from '../reducers/homeReducer';
import loginReducer from '../reducers/loginReducer';
import usuarioReducer from '../reducers/usuarioReducers';

const rootReducer = combineReducers({  
     load:loginReducer,
     message:messageReducer,
     requestloading:loadingReducer,
     home:homeReducer,
     usuario:usuarioReducer, 
});

export default rootReducer;
