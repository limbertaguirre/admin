import React from 'react';
import {
    Button, Dialog, DialogContent, Typography,Grid, MenuItem, InputLabel, FormControl, Select, TextField
} from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';

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
  }));


const NuevoDescuentoModal = (props) => {
   const { open , closeHandelModal,confirmarDecuento, buscarProducto, onChange, producto, monto, descripcion, cantidad, idProyecto, proyectoNombre,errorProducto, errorMonto,errorCantidad,  errorDescripcion, isValidForm }= props;
   const classes = useStyles();


  return (
    <>
        <Dialog
            fullWidth={true}               
            open={open}
            aria-labelledby="customized-dialog-title"            
        >            
            <DialogContent>
                        <div className={classes.root}>                        
                            <Typography variant="subtitle1" className={classes.TextFiel} gutterBottom>
                                <b>NUEVO DESCUENTO </b>
                            </Typography>    
                            <br />                                                                    
                        </div>   
                        <Grid  container  >
                            <Grid item xs={6}>
                                     <TextField                            
                                        label=" Producto"
                                        type={'text'}
                                        variant="outlined"                                      
                                        name="producto"
                                        value={producto}                                        
                                        className={classes.TextFiel}
                                        onChange={onChange}
                                        error={errorProducto}
                                        helperText={ errorProducto &&
                                        "campo requerido"
                                        }                            
                                        fullWidth    
                                        onKeyPress={(ev) => {
                                            if (ev.key === 'Enter') {
                                              buscarProducto();
                                            }
                                          }}                               
                                    />
                            </Grid>
                            <Grid item xs={6}>
                                        <TextField                            
                                        label=" Monto"
                                        type={'number'}
                                        variant="outlined"                                      
                                        name="monto"
                                        value={monto}                                         
                                        className={classes.TextFiel}
                                        onChange={onChange}
                                        error={errorMonto}
                                        helperText={ errorMonto &&
                                        "El campo es requerido"
                                        }                           
                                        fullWidth                             
                                    />

                            </Grid>      
                            {/* {idProyecto>0&& */}                      
                            <Grid item xs={12}> 
                                <TextField                            
                                        label=" Complejo"
                                        type={'text'}
                                        variant="outlined"                                      
                                        name="Complejo"
                                        disabled
                                        value={proyectoNombre} 
                                        className={classes.TextFiel}                                      
                                        fullWidth                             
                                    />
                            </Grid>
                           {/*  } */}
                            {idProyecto<=0&&    
                            <Grid item xs={12}> 
                                <TextField                            
                                        label=" Cantidad"
                                        type={'number'}
                                        variant="outlined"                                      
                                        name="cantidad"                                        
                                        value={cantidad}                                       
                                        className={classes.TextFiel}
                                        onChange={onChange}
                                        error={errorCantidad}
                                        helperText={ errorCantidad &&
                                        "Debe ingresar una cantidad mínima."
                                        }                         
                                        fullWidth                             
                                    />
                            </Grid>
                            }
                            <Grid item xs={12}>
                                        <TextField                            
                                        label=" Descripcion"
                                        type={'text'}
                                        variant="outlined"                                      
                                        name="descripcion"
                                        value={descripcion}                                   
                                        className={classes.TextFiel}
                                        onChange={onChange}
                                        error={errorDescripcion}
                                        helperText={ errorDescripcion &&
                                        "La descripción debe tener como mínimo 20 caracteres."
                                        }          
                                        multiline
                                        rows={4}                  
                                        fullWidth                             
                                    />
                            </Grid>

                        </Grid>                
                    <Grid  container item xs={12}  justify="flex-end"  >
                        <Button  onClick={closeHandelModal} variant="contained" color="primary" className={classes.botones} >
                            Cancelar
                        </Button>
                        <Button disabled={!isValidForm()}  onClick={confirmarDecuento} variant="contained" color="primary" className={classes.botones}>
                            Aceptar
                        </Button>
                    </Grid>
            </DialogContent>             
        </Dialog>
    </>
  );
};
export default NuevoDescuentoModal;
