

import React,{useState, useEffect }  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { makeStyles, emphasize, withStyles  } from '@material-ui/core/styles';
import { useSelector,useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";

import Chip from '@material-ui/core/Chip';


import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
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
    const dispatch = useDispatch();
    const history = useHistory();

    const handleChange = (event) => {
        console.log("selecciono ", event.target.checked, " : ", modulo);
         if(event.target.checked){
            // selecionoPagina(pagina,modulo.idModulo,modulo.nombre)
         }else{
            // desSelecionoPagina(pagina, modulo.idModulo,modulo.nombre)
         }
     };

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
            {/* <FormControlLabel
                aria-label="Acknowledge"
                onClick={handleChange}
               // onFocus={(event) => event.stopPropagation()}
                control={<Checkbox />}
                label={modulo.nombre}
            /> */}
            </AccordionSummary>
            <AccordionDetails>
                <div className={style.rootAcordion}>
                    {modulo.listmodulos.map((value, index)=>{                               
                        return( 
                            <EditAcordionPagina pagina={value} idModulo={modulo.idModulo} nombreModulo={modulo.nombre} selecionoPermiso={selecionoPermiso} desSelecionoPermiso={desSelecionoPermiso} />
                        )
                     })}                           
                </div>  
            </AccordionDetails>
            </Accordion>
                                                   
         </>
    );
}
export default  EditAcordionModulo;

