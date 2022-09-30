import * as Type from '../types/cargarAplicacionesType';

const defaultState = {
    ciclosList: [],
    listPaises:[],
    listCiudades:[],
    listBajas:[],
    listBancos:[],
    objCliente:{
        apellido: "",
        avatar: "",
        ci: "",
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
        razonSocial: "",
        telFijo: "",
        telMovil: "",
        telOficina: "",
        tieneCuentaBancaria: false
    }
};

export default function reducer (state = defaultState, action) {  
    switch (action.type) {       
        case Type.LISTA_CICLOS:
            return{
                ...state,
                ciclosList:action.ciclosList,
            };
        case Type.LISTA_APLICACIONES:
            return{
                ...state,
                ciclosList:action.ciclosList,
            }
        default: {
            return state;
        }
    }
};

