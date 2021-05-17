import * as Types from '../types/usuarioType'
import {requestGet, requestPost} from '../../service/request';
import * as Action from './messageAction';


const listaPaginas=[{
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
]


export const getPaginas= ()=>{
    return (dispatch)=>{     
         dispatch({
             type: Types.LISTA_PAGINAS,
             paginas:listaPaginas,
         })
                  
    }
  }