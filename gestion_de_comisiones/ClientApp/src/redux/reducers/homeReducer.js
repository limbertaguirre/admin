import * as Type from '../types/homeTypes';


const defaultState = {
    principal: [],    
    loadMore:false,
    menu:[],
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.NO_MORE_PAGE:
            return{
                ...state,
                loadMore:false,
            }
        case Type.MENU_PAGE:
            return{
                ...state,
                menu:action.menu,
            }     
        default: {
            return state;
        }
    }
};

