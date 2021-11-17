import React from 'react';
import {
    Button, Dialog, DialogContent, Typography,Grid, TextField,FormLabel,  FormControlLabel ,FormControl, Radio, RadioGroup, FormHelperText, Tooltip ,Zoom,
} from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import MenuItem from "@material-ui/core/MenuItem";
import MenuList from "@material-ui/core/MenuList";
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import DoneAllIcon from '@material-ui/icons/DoneAll';
import ErrorOutlineIcon from '@material-ui/icons/ErrorOutline';
import { Alert, AlertTitle } from '@material-ui/lab';
import Paper from '@material-ui/core/Paper';
import ButtonBase from '@material-ui/core/ButtonBase';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';

//import LogoSion from '../../../../assets/icons/LogoSION.sgv'

const useStyles = makeStyles((theme) => ({
    root: {
      width: '100%',
      '& > * + *': {
        marginTop: theme.spacing(2),
      },
      display:'flex',
      alignItems:'center',
      justifyContent:'center',
      backgroundColor:'#E3F2F7',
      borderRadius: "3px",    
      marginBottom: theme.spacing(1)
    },
    divRadio: {
        width: '100%',
        '& > * + *': {
          marginTop: theme.spacing(2),
        },
        display:'flex',
        alignItems:'center',
        justifyContent:'center',
        //backgroundColor:'#E3F2F7',
        borderRadius: "3px",    
        marginBottom: theme.spacing(1)
      },
      group: {
        margin: `${theme.spacing.unit}px 0`,
      },
    botones:{
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(2),
        marginTop:theme.spacing(2),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    },
    botonesSecondary:{
        background: "#f44336", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(2),
        marginTop:theme.spacing(2),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    },
    TextFiel: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
        marginRight:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingLeft:theme.spacing(1),
        //width:'98%'
    },
    Titulo: {
        marginBottom: theme.spacing(2),
        marginTop: theme.spacing(2),
        marginRight:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingLeft:theme.spacing(1),
        //width:'98%'
    },
    titleDescripcion:{
      color:'red',  
    },
    root2: {
        flexGrow: 1,
    },
    paper: {
        padding: theme.spacing(2),
        margin: 'auto',
        maxWidth: 500,
    },
    image: {
        width: 110,
       // height: 128,
       display:'flex',
       alignItems:'center',
       justifyContent:'center',
    },
    img: {
        margin: 'auto',
        display: 'block',
        maxWidth: '100%',
        maxHeight: '100%',
    },
    centrarDiv: {
       /*  width: '100%',
        '& > * + *': {
          marginTop: theme.spacing(2),
        }, */
       // display:'flex',
        alignItems:'center',
       
        backgroundColor:'#E3F2F7',
       // borderRadius: "3px",    
      //  marginBottom: theme.spacing(1)
      },
    
  }));


const ConfirmarCierrePagoModal = (props) => {
   const { open , closeHandelModal,confirmarPago, listado, habilitado, listadoSeleccionado }= props;
   const classes = useStyles();

  return (
    <>
        <Dialog
            fullWidth={true}               
            open={open}
            aria-labelledby="customized-dialog-title"            
        >            
            <DialogContent>
                        <Alert severity={"warning"}>                                               
                              <AlertTitle>  {habilitado? 'CONFIRMACION CIERRE DE PAGO':'PENDIENTE AUTORIZAR '}</AlertTitle>
                                <br /> 
                                 {habilitado&&
                                 <div >        
                                    <ul>
                                        {listadoSeleccionado.map((valu, index) => ( 
                                            <li key={index}> <b>{valu.nombre.toUpperCase()} {valu.cantidad} </b> ACI </li>
                                        ))}                                                                        
                                    </ul>    
                                               
                                    <Typography variant="caption" display="block" gutterBottom>
                                        <strong>NOTA :</strong> {'¿Está seguro de que desea cerrar la planilla?'} 
                                    </Typography>                                                                      
                                </ div>
                                 }                                                                                                                               
                        </Alert>                    
                                 
                        {listado.length >0 &&
                        <>
                            <br/>
                            <div className={classes.root2}>
                                <Typography gutterBottom variant="subtitle1">
                                    <b> {'AREAS QUE AUTORIZAN'} </b>
                                </Typography>
                                <Paper className={classes.paper}>
                                  {listado.map((valu, index) => ( 
                                      <div key={index} >
                                        <Grid container spacing={2}>                                        
                                            <Grid item xs={12} sm container>
                                                <Grid item xs container direction="column" spacing={2}>
                                                    <Grid item xs>
                                                        <Typography gutterBottom variant="subtitle1">
                                                        <b> {valu.area.toUpperCase()} {' ('} {valu.cantidadHabilitados} {' Personas '} {valu.cantidadConfigMinima} {'  )'}  </b>
                                                        </Typography>
                                                        {valu.listaAutorizadores.map((value, index2) => (   
                                                            <div key={index2}>
                                                            <Typography variant="body2" color="textSecondary">
                                                                {value.nombre.toUpperCase() +' '+ value.apellido.toUpperCase()} • {' '} {value.aprobado&&<DoneAllIcon  />}
                                                            </Typography>
                                                            </div>
                                                        ))}                                                                                                  
                                                    </Grid>                                                        
                                                </Grid>                                          
                                            </Grid>
                                            <Grid item>
                                                <ButtonBase className={classes.image}>                     
                                                {valu.habilitado&& <CheckCircleOutlineIcon fontSize={'large'} style={{color:'#1D7C0E'}}  /> }
                                                </ButtonBase>
                                            </Grid>
                                        </Grid>                                      
                                        { index+1 != listado.length && <hr />}
                                       </ div>
                                   ))}
                                </Paper>                                                                                             
                            </div>
                        </>
                        }
                    <Grid  container item xs={12}  justify="flex-end"  >
                        <Button  onClick={closeHandelModal} variant="contained" color="primary" className={classes.botonesSecondary}  >
                           {habilitado?  'Cancelar': 'Cerrar'}
                        </Button>
                        {habilitado&&
                            <Button  onClick={confirmarPago} variant="contained" color="primary" className={classes.botones} >
                               Confirmar
                            </Button>
                        }
                    </Grid>
            </DialogContent>             
        </Dialog>
    </>
  );
};
export default ConfirmarCierrePagoModal;
