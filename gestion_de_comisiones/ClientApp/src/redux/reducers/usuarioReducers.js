import * as Type from '../types/usuarioType';


const defaultState = {
    loadMore:false,
    listModulos: [],  
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.LISTA_PAGINAS:
            return{
                ...state,
                listModulos:action.paginas,
            }
   
        default: {
            return state;
        }
    }
};

