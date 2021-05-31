

import React,{useState, useEffect}  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { Dialog, DialogContent, Button, Grid } from "@material-ui/core"
import { makeStyles } from '@material-ui/core/styles';
import { verificaAlfanumerico } from "../../lib/expresiones";
import { useSelector,useDispatch } from "react-redux";
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';
import Paper from '@material-ui/core/Paper';
import CheckPermiso from './CheckPermiso';


const useStyles = makeStyles((theme) => ({

   
      heading: {
        fontSize: theme.typography.pxToRem(15),
        flexBasis: '33.33%',
        flexShrink: 0,
      },
      
}));


const  CheckPermisoPadre =(props)=>  { 
    const {lPermisos, selecionoPermiso, desSelecionoPermiso, cambiarEstadoPermisoObjList  } = props     
    const style = useStyles();  
    
    return (
         <>            
                {lPermisos.map((value2,index) => {
                const labelId = `transfer-list-item-${value2.id_permiso}-label`;
                return (
                  <ListItem key={value2.id_permiso} role="listitem" button 
                  //onClick={handleToggle(value.id_pagina,value.nombre )}
                  > 
                  { /*
                 <CheckPermiso permiso={value2} labelId={labelId} selecionoPermiso={selecionoPermiso} desSelecionoPermiso={desSelecionoPermiso} cambiarEstadoPermisoObjList={cambiarEstadoPermisoObjList} />
                  */}
                  </ListItem>
                );
                })}
         </>
    );
}
export default  CheckPermisoPadre;

