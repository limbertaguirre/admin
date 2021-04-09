import { combineReducers } from "redux";
import messageReducer from './messageReducer';
import loginReducer from './loadingReducer';
import homeReducer from '../reducers/homeReducer';

const rootReducer = combineReducers({  
     message:messageReducer,
     load:loginReducer,
     home:homeReducer, 
});

export default rootReducer;
