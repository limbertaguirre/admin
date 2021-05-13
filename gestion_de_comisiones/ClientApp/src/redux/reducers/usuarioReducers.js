import * as Type from '../types/usuarioType';


const defaultState = {
    loadMore:false,
    listAreas: [],  
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.LISTA_AREAS:
            return{
                ...state,
                listAreas:action.areas,
            }
   
        default: {
            return state;
        }
    }
};

