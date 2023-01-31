import * as Type from '../types/homeTypes';


const defaultState = {
    principal: [],    
    loadMore:false,
    menu:[],
    perfiles:[],
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
                perfiles:action.perfiles,
            }    
        case Type.MENU_PAGE_CLEAR:
            return{
                ...state,
                menu:[],
                perfiles:[]
            }  
        default: {
            return state;
        }
    }
};

