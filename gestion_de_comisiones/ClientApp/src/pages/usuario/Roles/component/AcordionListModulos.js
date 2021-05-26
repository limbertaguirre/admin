

import React  from 'react';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    icono: {
        width: '40px',
        height: '40px',
    }
      
}));


const  AcordionListModulos =(props)=>  { 
    const {listHisotrico  } = props     
    const style = useStyles();
     //console.log('lista : ', listHisotrico);
    return (
         <>    
            {listHisotrico.map((value,index) => {
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

