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
    globalModules:[],
    objetoRol:{
        idRol:0,
        nombre:'',
        descripcion:'',
        listModulos:[]
    },
    
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
        case Type.LISTA_GLOBAL_ROLES_MODULOS:
            return{
                ...state,
                globalModules:action.globalModules
            }
        case Type.OBJETO_GLOBAL_ROLES_MODULOS:
            return{
                ...state,
                objetoRol:action.objetoRol
            }
   
        default: {
            return state;
        }
    }
};

