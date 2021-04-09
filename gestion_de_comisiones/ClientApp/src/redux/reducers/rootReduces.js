import { combineReducers } from "redux";
import messageReducer from './messageReducer';
import homeReducer from '../reducers/homeReducer';

const rootReducer = combineReducers({  
     message:messageReducer,
     home:homeReducer, 
});

export default rootReducer;
