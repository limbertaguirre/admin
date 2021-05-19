

import React,{useState, useEffect}  from 'react';

import { makeStyles } from '@material-ui/core/styles';



const useStyles = makeStyles((theme) => ({
    icono: {
        width: '40px',
        height: '40px',
    },
    contentTitle: {
        textAlign: 'rigth',
    },
    contentButton: {
        textAlign: 'center',
    },
    contentDialog: {
         background: '#bgfg55',
        width: '70%',
    },
    TextFiel: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
        width: '50%',
    },
    formControl: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
    },
    //-------------------------------
  //--estilo de transferencia
    root: {
        margin: 'auto',
        width: '100%',
      },
      paper: {
        width: '500px',
        height: '700px',
        overflow: 'auto',
      },
      button: {
        margin: theme.spacing(0.5, 0),
      },
      ///--------------------------------
      heading: {
        fontSize: theme.typography.pxToRem(15),
        flexBasis: '33.33%',
        flexShrink: 0,
      },
      rootAcordion: {
        width: '100%',
      },
      acordionStilo:{
        backgroundColor: 'rgba(0, 0, 0, .03)',
       
      }
      
}));


const  HistoryModel =(props)=>  { 
    const {listHisotrico  } = props     

    

   
  
  

    return (
         <>    
            {listHisotrico.map((value,index) => {
                return (
                    <ul>
                        <li><b>Modulo :</b> {value.nombreModulo}
                            <ul>
                                {value.paginas.map((value1,index) => {
                                    return (
                                      <li><b>Pagina :</b> {value1.nombrePagina}
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
export default  HistoryModel;

