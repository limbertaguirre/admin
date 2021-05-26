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
        requestGet('Rol/Listamodulos',{},dispatch).then((res)=>{ 
            if(res.code === 0){
                dispatch({
                    type: Types.LISTA_PAGINAS,
                    paginas:res.data,
                })
                
            }else{
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }   
        })

        requestGet('Rol/Listapermisos',{},dispatch).then((res)=>{ 
            if(res.code === 0){
                dispatch({
                    type: Types.LISTA_PERMISOS,
                    permisos:res.data
                })
                
            }else{
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }   
        })      
           //-------------------------------------------
           //---aqui prueba consumir id Rol
               // const headers={idRol:14};
               // requestGet(`Rol/ObtenerListaXRol`,headers,dispatch).then((res)=>{ 
                 //   if(res.code === 0){
                 //   console.log('rolesss : ', res)
                        
                  //  }else{
                  //      dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                   // }   
                  //  })    
           //------------------------------------------- 

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
  export const ObtenerRolesModulos= ()=>{
    return (dispatch)=>{        
      //---todos los roles mas modulos
            requestGet('Rol/ObtenerRolesAllModules',{},dispatch).then((res)=>{ 
                if(res.code === 0){
                console.log('get all roles : ', res.data)
                    dispatch({
                        type: Types.LISTA_GLOBAL_ROLES_MODULOS,
                        globalModules:res.data
                    })                    
                }else{
                    dispatch(Action.showMessage({ message: res.message, variant: "error" }));
                }   
    })    

    }
  }
  export const ObtenerRolModulos= (idRolSelecionado)=>{
    return (dispatch)=>{        
         const headers={idRol:idRolSelecionado};
        requestGet(`Rol/ObtenerListaXRol`,headers,dispatch).then((res)=>{ 
            if(res.code === 0){
            console.log('rolesss : ', res.data)
               dispatch({
                   type:Types.OBJETO_GLOBAL_ROLES_MODULOS,
                   objetoRol:res.data
                 }) 
                
            }else{
                dispatch(Action.showMessage({ message: res.message, variant: "error" }));
            }   
            })    
    }
  }
