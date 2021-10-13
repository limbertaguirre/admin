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
    
  }));


const ConfirmarCierrePagoModal = (props) => {
   const { open , closeHandelModal,confirmarPago, listado, habilidado }= props;
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
                        
                       
                              <AlertTitle>  {habilidado? 'COFIRMAR CIERRE PAGO ':'PENDIENTE A AUTORIZAR '}</AlertTitle>
                                    <br /> 
                                {!habilidado&&                          
                                <Typography variant="caption" display="block" gutterBottom>
                                    <strong>NOTA :</strong> {'Esta seguro que desea cerrar el pago.'} 
                                </Typography>
                                 }                                                                                                                               
                        </Alert>                    
                        <br />   
                        <Grid  container  >                                                     
                            <div className={classes.divRadio}>
                                <List className={classes.rootList}>
                                    {listado.map((valu, index) => (
                                        <ListItem key={index}>                                        
                                            <ListItemText primary={valu.nombre.toUpperCase() +' '+ valu.apellido.toUpperCase()} />
                                            <ListItemAvatar>
                                            {valu.aprobado? <DoneAllIcon style={{color:'#1D7C0E'}} /> :
                                                <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={'Pendiente a aprobacion'}>
                                                <ErrorOutlineIcon color="disabled" /> 
                                                </Tooltip>
                                                }
                                            </ListItemAvatar>
                                        </ListItem>                                      
                                    ))}                    
                                </List>                                                
                            </div>                               
                        </Grid>                
                    <Grid  container item xs={12}  justify="flex-end"  >
                        <Button  onClick={closeHandelModal} variant="contained" color="primary" className={classes.botones}  >
                           {habilidado?  'Cancelar': 'Cerrar'}
                        </Button>
                        {habilidado&&
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
