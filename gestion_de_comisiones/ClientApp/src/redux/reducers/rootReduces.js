import { combineReducers } from "redux";
import messageReducer from './messageReducer';
import loadingReducer from './loadingReducer';
import homeReducer from '../reducers/homeReducer';
import loginReducer from '../reducers/loginReducer';

const rootReducer = combineReducers({  
     load:loginReducer,
     message:messageReducer,
     requestloading:loadingReducer,
     home:homeReducer, 
});

export default rootReducer;
