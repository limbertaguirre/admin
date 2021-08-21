

import React  from 'react';
import ViewModuleIcon from '@material-ui/icons/ViewModule';
import WebIcon from '@material-ui/icons/Web';

const  ListSelectModules =({listaSelecionada})=>  {   
  
    return (
         <>    
            {listaSelecionada.map((value,index) => {
                return (
                    <ul style={{listStyle:'none'}}>
                        <li style={{listStyle:'none'}}> <ViewModuleIcon  fontSize="small" /> <b>Módulo :</b> {value.nombre}
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

