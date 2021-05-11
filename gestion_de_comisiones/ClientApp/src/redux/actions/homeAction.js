import * as Types from '../types/homeTypes';
import {requestGet} from '../../service/request';

const menuWeb=[
   {
    titleMenu:'Gestion de pagos',
    iconMenu:'gestionPago',
     listaMenu:[ {
                    titleSubMenu:'Pagos de comsiones',
                    iconsSubMenu:'pago',
                    listaSubMenu:[
                        {
                          idpage:1,
                          title:'facturacion',
                          descripion:'aqui se mostrara el proceso de facturacion',
                          namePage:'Facturacion',
                          path:'/facturacion',
                          icon:'factura'            
                        },
                        {
                          idpage:2,
                          title:'Cargar comisiones',
                          descripion:'',
                          namePage:'cargaComisiones',
                          path:'/cargar/Comisiones',
                          icon:'factura'            
                        },
                        {
                          idpage:3,
                          title:'Prorrateo',
                          descripion:'aqui se mostrara el proceso de facturacion',
                          namePage:'prorrateo',
                          path:'/prorrateo',
                          icon:'prorrateo'            
                        },
                        {
                          idpage:4,
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
         console.log("se ejecuto el load")
       })    
  }
}
export const cargarMenu= ()=>{
  return (dispatch)=>{   
      
       dispatch({
        type: Types.MENU_PAGE,
         menu:menuWeb,
       })
    
  }
}
