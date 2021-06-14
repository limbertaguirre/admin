import * as Type from '../types/clienteTypes';


const defaultState = {
    listClientes: [],
    objCliente:{}
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.LISTA_CLIENTES:
            return{
                ...state,
                listClientes:action.listClientes,
            }
        case Type.BUSQUEDA_NOMBRE_CLIENTE:
            return{
                ...state,
                listClientes:action.listClientes,
            }    
        case Type.CLEAR_LIST_CLIENTES:
            return{
                ...state,
                listClientes:[],
            }  
        case Type.OBJETO_CLIENTE:
            return{
                ...state,
                objCliente:action.objCliente,
            }  
        case Type.CLEAR_OBJETO_CLIENTE:
            return{
                ...state,
                objCliente:{}, //aqui incializar campos
            } 
        default: {
            return state;
        }
    }
};

