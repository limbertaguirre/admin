

import React  from 'react';
import {  Typography } from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import EditAcordionPermiso from './EditAcordionPermiso';
import Grid from '@material-ui/core/Grid';


import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import WebIcon from '@material-ui/icons/Web';

const useStyles = makeStyles((theme) => ({
      root: {
       // width: '100%',
        flexGrow: 1,
      },
      title: {
        fontSize: 14,
        textTransform: 'uppercase'
        //color:'#FFFFFF'
      },
      fondo:{
       // background: "linear-gradient(90deg, #2E3B55, #1872b8)",
      }
}));

const  EditAcordionPagina =({pagina, idModulo, nombreModulo, selecionoPermiso, desSelecionoPermiso})=>  {       
    const style = useStyles();
    const dispatch = useDispatch();
    const history = useHistory();

    const handleChange = (event) => {
       
         if(event.target.checked){
            // selecionoPagina(pagina,modulo.idModulo,modulo.nombre)
         }else{
            // desSelecionoPagina(pagina, modulo.idModulo,modulo.nombre)
         }
     };

    return (
         <>    
            <Accordion className={style.fondo}>
            <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-label="Expand"
            aria-controls="additional-actions1-content"
            id="additional-actions1-header"
            >
                <Typography className={style.title} color="textSecondary" gutterBottom>
                 <WebIcon fontSize="small" /><b> PAGINA : </b> {pagina.nombre}
                </Typography>
            </AccordionSummary>
            <AccordionDetails>            
                <div className={style.root}>                    
                    <Grid container spacing={3}>
                        {pagina.permisos.map((value, index)=>{                               
                            return( 
                                <EditAcordionPermiso key={index} permiso={value} idModulo={idModulo} nombreModulo={nombreModulo} pagina={pagina} selecionoPermiso={selecionoPermiso} desSelecionoPermiso={desSelecionoPermiso} />
                            )
                        })}  
                    </Grid>                         
                </div>              
            </AccordionDetails>
            </Accordion>
                                                   
         </>
    );
}
export default  EditAcordionPagina;

