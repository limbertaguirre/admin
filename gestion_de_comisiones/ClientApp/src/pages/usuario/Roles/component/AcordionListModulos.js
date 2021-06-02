

import React, { useState }  from 'react';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    icono: {
        width: '40px',
        height: '40px',
    }
      
}));


const  AcordionListModulos =(props)=>  { 
    const {listHisotrico  } = props   
    const [listaFiltrada,setListaFiltrada] =useState([]); 
    const style = useStyles();
    const recargarModulos =(todosModulo)=>{
        let global=[];
        
        let nroModules=todosModulo.length;
        let newListModulos=[];
        for(let i=0; i<nroModules; i++){
               let objModulo=todosModulo[i];
               let nroPagina=objModulo.listmodulos.length;               
               let newLisPaginas=[];
               for(let pa=0;  pa<nroPagina;  pa++){
                       let objPagina= objModulo.listmodulos[pa];
                       let nroPermiso= objPagina.permisos.length;
                       let newListPermisos=[];
                       for(let pe=0; pe<nroPermiso; pe++){
                         let objPermiso= objPagina.permisos[pe];
                         if(objPermiso.estado == true){                         
                           newListPermisos.push(objPermiso);
                         }
                       }
                       //crear pagina y add permisos
                       if(newListPermisos.length != 0){//addPagina
                         const newObjPagina={
                           id_pagina: objPagina.id_pagina,
                           nombre: objPagina.nombre,
                           permisos: newListPermisos
                         }
                         newLisPaginas.push(newObjPagina)
                        
                       }
                   
               }
               if(newLisPaginas.length != 0){//addModulo
                 const newobjModelo={
                   idModulo: objModulo.idModulo,
                   nombre: objModulo.nombre,
                   listmodulos:newLisPaginas,
                 }
                 newListModulos.push(newobjModelo)                 
               }  
        }
        return newListModulos
      
     }
     let listado=recargarModulos(listHisotrico);
    return (
         <>    
            {listado.map((value,index) => {
                return (
                    <ul>
                        <li><b>Modulo :</b> {value.nombre}
                             <ul>
                                {value.listmodulos.map((value1,index) => {
                                    return (
                                      <li><b>Pagina :</b> {value1.nombre}
                                         <ul>
                                            {value1.permisos.map((value2,index) => {
                                                    return (
                                                      <li>{value2.permiso}</li>
                                                    );
                                                })}  
                                         </ul>                                    
                                      </li>                                     
                                    )
                                })}  
                            </ul> 
                        </li>  
                    </ul>
                );
           })}        
           
         </>
    );
}
export default  AcordionListModulos;
