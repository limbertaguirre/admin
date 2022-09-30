

import React,{useState, useEffect}  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles } from '@material-ui/core/styles';
import { verificaAlfanumerico } from "../../lib/expresiones";
import { useSelector,useDispatch } from "react-redux";
import * as Action from '../../redux/actions/usuarioAction'
//-----------------------transferencia
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';
import Paper from '@material-ui/core/Paper';


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
        height: '300px',
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


const  CheckPagina =(props)=>  { 
    const {modulo, pagina, labelId, selecionoPagina, desSelecionoPagina  } = props     
    const style = useStyles();
    const dispatch = useDispatch();

    const [checked, setChecked] = React.useState(false);
    

    const handleChange = (event) => {
      
        if(event.target.checked){
            selecionoPagina(pagina,modulo.idModulo,modulo.nombre)
        }else{
            desSelecionoPagina(pagina, modulo.idModulo,modulo.nombre)
        }
      setChecked(event.target.checked);
    };
  
  

    return (
         <>            
              <ListItemIcon>
                <Checkbox
                    checked={checked}
                    onChange={handleChange} 
                    disableRipple 
                    inputProps={{ 'aria-labelledby':labelId }}
                />
                </ListItemIcon>
                <ListItemText id={labelId} primary={`${pagina.nombre}`} />
         </>
    );
}
export default  CheckPagina;

