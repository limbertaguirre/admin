import * as Type from '../types/clienteTypes';


const defaultState = {
    listClientes: [],
    listPaises:[],
    listCiudades:[],
    listBajas:[],
    listBancos:[],
    objCliente:{
        apellido: "",
        avatar: "",
        ci: "34343434",
        codigo: "",
        codigoBanco: "",
        codigoPatrocinador: "",
        comentario: '',
        contrasena: '',
        correoElectronico: '',
        cuentaBancaria: '',
        direccion: '',
        estado: 0,
        facturaHabilitado: false,
        fechaBaja: "2021-01-01T00:00:00",
        fechaNacimiento: "1900-10-10T00:00:00",
        fechaRegistro: "2021-01-01T00:00:00",
        idBanco: 0,
        idCiudad: 0,
        idFicha: 0,
        idFichaTipoBajaDetalle: 0,
        idNivelDetalle: 0,
        idPais: 0,
        idTipoBaja: 0,
        motivoBaja: "",
        nit: "",
        nivel: "",
        nombre: "",
        nombreBanco: "",
        nombrePatrocinador: "",
        razonSocial: null,
        telFijo: "",
        telMovil: "",
        telOficina: "",
        tieneCuentaBancaria: false
    }
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
        case Type.LISTA_PAISES:
            return{
                ...state,
                listPaises:action.listPaises
            } 
        case Type.LISTA_CIUDADES:
            return{
                ...state,
                listCiudades:action.listCiudades
            } 
        case Type.LISTA_BAJAS:
            return{
                ...state,
                listBajas:action.listBajas
            } 
        case Type.LISTA_BANCOS:
            return{
                ...state,
                listBancos:action.listBancos
            } 
        default: {
            return state;
        }
    }
};

