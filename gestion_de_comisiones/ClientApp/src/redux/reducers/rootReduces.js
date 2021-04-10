import { combineReducers } from "redux";
import messageReducer from './messageReducer';
import loadingReducer from './loadingReducer';
import homeReducer from '../reducers/homeReducer';

const rootReducer = combineReducers({  
     message:messageReducer,
     requestloading:loadingReducer,
     home:homeReducer, 
});

export default rootReducer;
