import * as Type from '../types/loginTypes';

const defaultState={
    load:false,
    userName:'',
    loadFail:false,
    modalUserNew:false,
    listAreas: [],  
    listSucursales:[],
    idUsuario:0,
    nombre:'',
    apellido:''
};

export default function reducer(state = defaultState, action){
    switch (action.type){
        case Type.LOAD_LOGIN:
            return {
              ...state,
              load:true,              
              userName:action.userName,
              idUsuario:action.idUsuario,
              nombre:action.nombre,
              apellido:action.apellido
            };
        case Type.LOAD_LOGIN_ERROR:
            return {

                ...state,
                load:false,
                userName:'',
                loadFail:true,
                idUsuario:0,
                nombre:'',
                apellido:''                
            };
        case Type.CLOSE_SESION:
            return {
                ...state,
                load:false,
                userName:'',
                loadFail:false,
                idUsuario:0,
            };
        case Type.OPEN_MODAL_USER:
            return {
                ...state,
                modalUserNew:true
            };
        case Type.CLOSE_MODAL_USER:
            return {
                ...state,
                modalUserNew:false
            };
        case Type.LISTA_AREAS:
            return{
                ...state,
                listAreas:action.areas,
            }
        case Type.LISTA_SUCURSALES:
            return{
                ...state,
                listSucursales:action.sucursales,
            }     
        default:{
            return state;   
        }
    }
};