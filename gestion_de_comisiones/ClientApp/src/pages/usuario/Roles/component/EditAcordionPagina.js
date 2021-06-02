

import React,{useState, useEffect }  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { makeStyles, emphasize, withStyles  } from '@material-ui/core/styles';
import { useSelector,useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import EditAcordionPermiso from './EditAcordionPermiso';
import Chip from '@material-ui/core/Chip';
import Grid from '@material-ui/core/Grid';


import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
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
   // console.log("id pagina ",pagina.nombre, pagina.id_pagina);
    //console.log(" pagina id modulo ",idModulo);

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
                 <WebIcon fontSize="small"s /><b> PAGINA : </b> {pagina.nombre}
                </Typography>
           {/*  <FormControlLabel
                aria-label="Acknowledge"
                onClick={handleChange}
               // onFocus={(event) => event.stopPropagation()}
                control={<Checkbox />}
                label={pagina.nombre}
            /> */}
            </AccordionSummary>
            <AccordionDetails>            
                <div className={style.root}>                    
                    <Grid container spacing={3}>
                        {pagina.permisos.map((value, index)=>{                               
                            return( 
                                <EditAcordionPermiso permiso={value} idModulo={idModulo} nombreModulo={nombreModulo} pagina={pagina} selecionoPermiso={selecionoPermiso} desSelecionoPermiso={desSelecionoPermiso} />
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

