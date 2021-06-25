import React, { Fragment } from "react";
import {
    Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Paper, Typography,Grid, Container
} from "@material-ui/core";
import EmojiObjectsIcon from "@material-ui/icons/EmojiObjects";
import { Alert, AlertTitle } from '@material-ui/lab';
import { makeStyles } from '@material-ui/core/styles';

import ListItemText from '@material-ui/core/ListItemText';
import ListItem from '@material-ui/core/ListItem';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import Slide from '@material-ui/core/Slide';


import Avatar from '@material-ui/core/Avatar';
import { blue  } from '@material-ui/core/colors';

const useStyles = makeStyles((theme) => ({
    root: {
      width: '100%',
      '& > * + *': {
        //marginTop: theme.spacing(2),
      },
    },
    botones:{
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(1),
        marginTop:theme.spacing(1),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    },
    appBar: {
        position: 'relative',
      },
      title: {
        marginLeft: theme.spacing(2),
        flex: 1,
      },
      avatarNombre: {
        color: theme.palette.getContrastText(blue[500]),
        backgroundColor: '#1872b8',
       // boxShadow: '2px 4px 5px #1872b8',
        width: theme.spacing(15),
        height: theme.spacing(15),
      },
      gridContainer:{
        paddingLeft:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingTop:theme.spacing(1),
        paddingBottom:theme.spacing(1),
      },
      submitCargar: {                 
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',
       },
       containerPhoto:{
        display:'flex',
        alignItems:'center',
        justifyContent:'center', 
      },
  }));
    const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
    });


const DetalleAdjuntoModal = ({ open,  handleCloseConfirm, handleCloseCancel }) => {
     //tipoModal : info, error, warning, success
     const classes = useStyles();

    let cerrarModal = () => {
        handleCloseConfirm();
    };
    const verFicha = () => {
      
    };

    return (
        <Fragment>
            <Dialog   fullWidth maxWidth="xl" open={open}   >
            <AppBar className={classes.appBar}>
                <Toolbar>
                    <IconButton edge="start" color="inherit" onClick={handleCloseCancel} aria-label="close">
                    <CloseIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                      DETALLE DE ADJUNTOS
                    </Typography>
                    <Button autoFocus color="inherit" onClick={handleCloseConfirm}>
                      GUARDAR
                    </Button>
                </Toolbar>
            </AppBar>     
               <Container maxWidth="md" >                          
                    <Grid  container item xs={12}  className={classes.gridContainer} >
                        <Grid item xs={12} md={3} className={classes.containerPhoto}  >
                           <Avatar alt="perfil"  className={classes.avatarNombre} > <h1> { 'MARIA'.charAt(0).toUpperCase() } </h1> </Avatar>
                        </Grid>
                        <Grid container xs={12} md={6}  >
                             <Grid  item xs={12}   >
                                    <Typography variant="h6" gutterBottom>
                                        MARIAR PEDRAZA
                                    </Typography>
                                    <Typography variant="subtitle1" gutterBottom>
                                    <b>RANGO:</b>  Embajador Internacional
                                    </Typography>
                                    <Typography variant="subtitle1" gutterBottom>
                                    <b>CICLO:</b>  CICLO ENERO
                                    </Typography>
                            </Grid>
                            <Grid  item xs={12}  justify="flex-end"  >
                            <Button
                                type="submit"                            
                                variant="contained"
                                color="primary"
                                className={classes.submitCargar}
                                onClick = {()=> verFicha()}                                         
                                >
                                VER
                                </Button>   
                            </Grid>
                        </Grid>
                        <br />
                         
                        <Grid  container item xs={12}  justify="flex-end"  >
                        
                        </Grid>

                    </Grid>
                </Container>   
            </Dialog> 
        </Fragment>
    );

};

export default DetalleAdjuntoModal;