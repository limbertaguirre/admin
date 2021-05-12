import * as Type from '../types/usuarioType';


const defaultState = {
    loadMore:false,
    listAreas: [],  
    listSucursales:[],
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.LISTA_AREAS:
            return{
                ...state,
                listAreas:action.areas,
            }
        case Type.LISTA_SUCURSALES:
            return{
                ...state,
                menu:action.sucursales,
            }     
        default: {
            return state;
        }
    }
};

