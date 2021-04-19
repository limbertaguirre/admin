import * as Types from '../types/homeTypes';
import {requestGet} from '../../service/request';
import * as Action from './messageAction'

const menuWeb=[
   {
     nameMenu:'Gestion de pagos',
     listaMenu:{
       nameSubMenu:'Pagos de comsiones',
       listaSubMenu:[
          {
            idpage:1,
            name:'facturacion',
            descripion:'aqui se mostrara el proceso de facturacion',
            namePage:'Facturacion',
            path:'/facturacion',
            icon:'factura'            
           },
           {
            idpage:2,
            name:'Cargar comisiones',
            descripion:'',
            namePage:'cargaComisiones',
            path:'/cargar/Comisiones',
            icon:'factura'            
           },
           {
            idpage:3,
            name:'prorrateo',
            descripion:'aqui se mostrara el proceso de facturacion',
            namePage:'Prorrateo',
            path:'/prorrateo',
            icon:'prorrateo'            
           },
           {
            idpage:4,
            name:'formaPago',
            descripion:'aqui se mostrara el proceso de facturacion',
            namePage:'Forma de pago',
            path:'/forma/pago',
            icon:'factura'            
           }




       ]
     }
   }
]

export const loadHome= ()=>{
  return (dispatch)=>{   
       requestGet('weatherforecast',{},dispatch).then((res)=>{
         console.log("ress : ", res);
           dispatch(Action.showMessage({ message: 'pruebaaa', variant: "error" }) );
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
