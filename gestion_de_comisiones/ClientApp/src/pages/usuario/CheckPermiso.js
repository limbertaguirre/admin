

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


const useStyles = makeStyles((theme) => ({

   
      heading: {
        fontSize: theme.typography.pxToRem(15),
        flexBasis: '33.33%',
        flexShrink: 0,
      },
      
}));


const  CheckPermiso =(props)=>  { 
    const { permiso, labelId, selecionoPermiso, desSelecionoPermiso  } = props     
    const style = useStyles();  
    const [checked, setChecked] = React.useState(false);
    

    const handleChange = (event) => {
        console.log("selecciono ", event.target.checked, " : ", permiso.permiso);
        if(event.target.checked){
          selecionoPermiso(permiso)
        }else{
          desSelecionoPermiso(permiso)
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
                <ListItemText id={labelId} primary={`${permiso.permiso}`} />
         </>
    );
}
export default  CheckPermiso;

