import { combineReducers } from "redux";
import homeReducer from '../reducers/homeReducer';

const rootReducer = combineReducers({  
     home:homeReducer, 
});

export default rootReducer;
