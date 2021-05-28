

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

import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import MuiDialogTitle from '@material-ui/core/DialogTitle';
import MuiDialogContent from '@material-ui/core/DialogContent';
import MuiDialogActions from '@material-ui/core/DialogActions';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import ListSelectModules from './ListSelectModules';
import Divider from '@material-ui/core/Divider';

const useStyles = makeStyles((theme) => ({
    root: {
        margin: 0,
        padding: theme.spacing(2),
      },
      closeButton: {
        position: 'absolute',
        right: theme.spacing(1),
        top: theme.spacing(1),
        color: theme.palette.grey[500],
      },
      grid: {
        display: 'flex',
        alignItems:"flex-end",
        justifyContent: 'flex-end',
    },
    botonAzul:{
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white'
    }
}));
const DialogTitle = withStyles(useStyles)((props) => {
    const { children, classes, onClose, ...other } = props;
    return (
      <MuiDialogTitle disableTypography className={classes.root} {...other}>
        <Typography variant="h6">{children}</Typography>
        {onClose ? (
          <IconButton aria-label="close" className={classes.closeButton} onClick={onClose}>
            <CloseIcon />
          </IconButton>
        ) : null}
      </MuiDialogTitle>
    );
  });

  const DialogContent = withStyles((theme) => ({
    root: {
      padding: theme.spacing(2),
    },
  }))(MuiDialogContent);
  
  const DialogActions = withStyles((theme) => ({
    root: {
      margin: 0,
      padding: theme.spacing(1),
    },
  }))(MuiDialogActions);

const  EditModalConfirm =({open, handConfirm, handleCloseModal, listaSelecionada })=>  { 
    const style = useStyles();
    return (
         <>     
            <Dialog onClose={handleCloseModal} aria-labelledby="customized-dialog-title" open={open}>
                <DialogContent dividers>
                     <Grid container spacing={4}>
                        <Grid item xs={12}>
                            <Typography variant="h6" gutterBottom>
                               Actualizar Roles Seleccionados 
                            </Typography>    
                            <Divider />                          
                        </Grid>
                        
                        <Grid item xs={12}>                        
                            <ListSelectModules listaSelecionada={listaSelecionada} />
                        </Grid>                       
                    </Grid>  
                </DialogContent>
                <DialogActions>
                    <Button variant="contained" className={style.botonAzul} onClick={handleCloseModal} >
                        Cancelar
                    </Button>
                    <Button variant="contained" className={style.botonAzul} onClick={handConfirm} >
                        Confirmar
                    </Button>
                </DialogActions>
            </Dialog>
         </>
    );
}
export default  EditModalConfirm;

