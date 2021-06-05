import * as Types from '../types/homeTypes';
import {requestGet} from '../../service/request';

const menuWeb=[
   {
     idMenu:1,
     titleMenu:'Gestion de pagos',
     iconMenu:'gestionPago',
     listaMenu:[ {
                    idSubMenu: 1,
                    titleSubMenu:'Pagos de comsiones',
                    iconsSubMenu:'pago',
                    listaSubMenu:[
                        {
                          idPage:1,
                          title:'facturacion',
                          descripion:'aqui se mostrara el proceso de facturacion',
                          namePage:'Facturacion',
                          path:'/facturacion',
                          icon:'factura'            
                        },
                        {
                          idPage:2,
                          title:'Cargar comisiones',
                          descripion:'',
                          namePage:'cargaComisiones',
                          path:'/cargar/Comisiones',
                          icon:'factura'            
                        },
                        {
                          idPage:3,
                          title:'Prorrateo',
                          descripion:'aqui se mostrara el proceso de facturacion',
                          namePage:'prorrateo',
                          path:'/prorrateo',
                          icon:'prorrateo'            
                        },
                        {
                          idPage:4,
                          title:'Forma de pago',
                          descripion:'aqui se mostrara el proceso de facturacion',
                          namePage:'formaPago',
                          path:'/forma/pago',
                          icon:'factura'            
                        }

                    ]
                  }
                  
                ]
   },
   
]

export const loadHome= ()=>{
  return (dispatch)=>{   
       requestGet('weatherforecast',{},dispatch).then((res)=>{
      
       })    
  }
}
export const cargarMenu= ()=>{
  return (dispatch)=>{   
      
     /*   dispatch({
        type: Types.MENU_PAGE,
         menu:menuWeb,
       }) */
    
  }
}
