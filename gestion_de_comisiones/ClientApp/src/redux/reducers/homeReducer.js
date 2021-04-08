import * as Type from '../types/homeTypes';


const defaultState = {
    principal: [],    
    loadMore:false,
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.NO_MORE_PAGE:
            return{
                ...state,
                loadMore:false,
            }    
        default: {
            return state;
        }
    }
};

