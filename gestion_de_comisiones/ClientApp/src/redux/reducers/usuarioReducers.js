import * as Type from '../types/usuarioType';


const defaultState = {
    loadMore:false,
    listModulos: [],  
    listPermisos: [],
    historico: [
        {
          idmodulo:0,
          nombreModulo:'',
          paginas:[
                { 
                  idpagina:0,
                  nombrePagina:'',
                  permisos:[
                            { 
                              idPermiso:0,
                              permiso:''
                            }
                  ]
                }
          ]
        }
    ],
    
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.LISTA_PAGINAS:
            return{
                ...state,
                listModulos:action.paginas,
            }
        case Type.LISTA_PERMISOS:
            return{
                ...state,
                listPermisos:action.permisos
            }
   
        default: {
            return state;
        }
    }
};

