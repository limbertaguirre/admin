

import React from 'react';
import {  Typography } from "@material-ui/core";
import { makeStyles  } from '@material-ui/core/styles';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import EditAcordionPagina from './EditAcordionPagina';
import ViewModuleIcon from '@material-ui/icons/ViewModule';

const useStyles = makeStyles((theme) => ({
      rootAcordion: {
        width: '100%',
      },
      title: {
        fontSize: 14,
        textTransform: 'uppercase',
        color:'#FFFFFF'
      },
      fondo:{
        background: "linear-gradient(90deg, #7194A4, #1872b8)",
      }
}));

const  EditAcordionModulo =({modulo, selecionoPermiso, desSelecionoPermiso})=>  {       
    const style = useStyles();
    return (
         <>    
            <Accordion
              className={style.fondo}
            >
            <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-label="Expand"
            aria-controls="additional-actions1-content"
            id="additional-actions1-header"
            >
                <Typography className={style.title} color="textSecondary" gutterBottom>
                    <ViewModuleIcon  fontSize="small" />    <b>MODULO :  {modulo.nombre}</b>  
                </Typography>
            </AccordionSummary>
            <AccordionDetails>
                <div className={style.rootAcordion}>
                    {modulo.listmodulos.map((value, index)=>{                               
                        return( 
                            <EditAcordionPagina key={index} pagina={value} idModulo={modulo.idModulo} nombreModulo={modulo.nombre} selecionoPermiso={selecionoPermiso} desSelecionoPermiso={desSelecionoPermiso} />
                        )
                     })}                           
                </div>  
            </AccordionDetails>
            </Accordion>
                                                   
         </>
    );
}
export default  EditAcordionModulo;

