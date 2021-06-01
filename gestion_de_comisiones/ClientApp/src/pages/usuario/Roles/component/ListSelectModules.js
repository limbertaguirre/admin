

import React  from 'react';
import { makeStyles } from '@material-ui/core/styles';
import ViewModuleIcon from '@material-ui/icons/ViewModule';
import WebIcon from '@material-ui/icons/Web';

const useStyles = makeStyles((theme) => ({
    icono: {
        width: '40px',
        height: '40px',
    }
      
}));


const  ListSelectModules =({listaSelecionada})=>  {   
    const style = useStyles();
    return (
         <>    
            {listaSelecionada.map((value,index) => {
                return (
                    <ul style={{listStyle:'none'}}>
                        <li style={{listStyle:'none'}}> <ViewModuleIcon  fontSize="small" /> <b>Modulo :</b> {value.nombre}
                             <ul style={{listStyle:'none'}}>
                                {value.listmodulos.map((value1,index) => {
                                    return (
                                      <li style={{listStyle:'none'}} >  <WebIcon fontSize="small"s /><b>Pagina :</b> {value1.nombre}
                                         <ul style={{listStyle:'none'}}>
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
export default  ListSelectModules;

