import * as Types from '../types/usuarioType'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';


const listaPaginas=[{
    idModulo:1,
    nombre:'gestion comisiones',
    listmodulos:[ 
        {
            id_pagina:1,
            nombre:'facturacion'
        },
        {
            id_pagina:2,
            nombre:'ficha cliente'
        },
        {
            id_pagina:3,
            nombre:'registro usuario'
        },
        {
            id_pagina:4,
            nombre:'cotizacion'
        }
    ],
  },
  {
    idModulo:2,
    nombre:'admin porrateo',
    listmodulos:[ 
        {
            id_pagina:5,
            nombre:'prorrateo'
        },
        {
            id_pagina:6,
            nombre:'registro empresa'
        },
        {
            id_pagina:7,
            nombre:'comisiones'
        }
    ],
  }
];

const permisos=[
    {
        id_permiso:1,
        permiso:'CREAR'
    },
    {
        id_permiso:2,
        permiso:'ACTUALIZAR'
    },
    {
        id_permiso:3,
        permiso:'ELIMINAR'
    },
    {
        id_permiso:4,
        permiso:'VISUALIZAR'
    }
];


export const getPaginas= ()=>{
    return (dispatch)=>{     
         dispatch({
             type: Types.LISTA_PAGINAS,
             paginas:listaPaginas,
             permisos:permisos
         })
                  
    }
  }

  export const registrarRoles= (nombre, descripcion,lista)=>{
    let body={
        nombre :nombre,
        descripcion:descripcion,
        idUsuario:100,
        modulos:lista
    };
    console.log('body :',body);
    return (dispatch)=>{        
        requestPost('Rol/Registrar',body,dispatch).then((res)=>{ 
            if(res.code === 0){
                dispatch(Action.showMessage({ message: res.message, variant: "success" }));
                window.location.replace('/gestion/roles');
            }else{
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }   
        })

    }
  }