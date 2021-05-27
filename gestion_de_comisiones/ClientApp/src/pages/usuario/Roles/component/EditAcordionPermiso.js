

import React,{useState, useEffect }  from 'react';
import { TextField, Typography, InputAdornment } from "@material-ui/core";
import { makeStyles, emphasize, withStyles  } from '@material-ui/core/styles';
import { useSelector,useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import Grid from '@material-ui/core/Grid';
import Checkbox from '@material-ui/core/Checkbox';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';

const useStyles = makeStyles((theme) => ({

      fondo:{
       // background: "linear-gradient(90deg, #2E3B55, #1872b8)",
      },
      root: {
        flexGrow: 1,
      },
      paper: {
        padding: theme.spacing(2),
        textAlign: 'center',
        color: theme.palette.text.secondary,
      },
}));

const  EditAcordionPermiso =({permiso, idModulo, pagina, selecionoPermiso, desSelecionoPermiso })=>  {       
    const style = useStyles();
    const dispatch = useDispatch();
    const [checked, setChecked] = React.useState(false);
    //const history = useHistory();
    //console.log("id pagina ",permiso.permiso);
   // console.log(" pagina id modulo ",idModulo);
    useEffect(()=>{
        setChecked(permiso.estado)
    },[])
    const handleChange = (event) => {
        //console.log('id modulo: ', idModulo, 'permiso :', permiso.permiso, 'estado :',event.target.checked)
        //console.log("selecciono ", event.target.checked, " : ");
        if(event.target.checked){
          selecionoPermiso(idModulo, pagina, permiso);
        }else{
         desSelecionoPermiso(idModulo, pagina, permiso);
        }
         setChecked(event.target.checked);
    };

    return (
         <>     
            <Grid item xs={3}>
               <List dense component="div" role="list">
                     <ListItem key={permiso.id_permisoa} role="listitem" button >
                            <ListItemIcon>
                            <Checkbox
                                checked={checked}
                                onChange={handleChange} 
                                disableRipple 
                                inputProps={{ 'aria-labelledby':permiso.id_permiso }}
                            />
                            </ListItemIcon>
                            <ListItemText id={permiso.id_permiso} primary={`${permiso.permiso}`} />
                    </ListItem>
                </List>
            </Grid>                                        
         </>
    );
}
export default  EditAcordionPermiso;

