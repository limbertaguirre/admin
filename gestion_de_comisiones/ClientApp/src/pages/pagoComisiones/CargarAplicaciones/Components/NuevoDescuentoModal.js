import React from 'react';
import {
    Button, Dialog, DialogContent, Typography,Grid, TextField,  FormControlLabel ,FormControl, Select, InputLabel, FormHelperText
} from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import Alert from '@material-ui/lab/Alert';
import MenuItem from "@material-ui/core/MenuItem";
import MenuList from "@material-ui/core/MenuList";


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
    
  }));


const NuevoDescuentoModal = (props) => {
   const { open , closeHandelModal,confirmarDecuento, buscarProducto, onChange, producto, monto, descripcion, cantidad, idProyecto, proyectoNombre,errorProducto, errorMonto,errorCantidad,  errorDescripcion, isValidForm, idTipoDescuento, errorIdTipoDescuento, listTipoDescuento }= props;
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
                                <Typography variant="subtitle1" className={classes.Titulo} gutterBottom>
                                    <b>NUEVO DESCUENTO </b>
                                </Typography>                                                                                              
                        </div>                       
                        <br />   
                        <Grid  container  >
                            <Grid item xs={12} >
                                <FormControl  variant="outlined"  
                                fullWidth 
                                className={classes.TextFiel}
                                >                                   
                                    <Select
                                        labelId="tipodescuento"                                        
                                        value={idTipoDescuento}
                                        name="idTipoDescuento"
                                        //error={errorIdTipoDescuento}
                                        onChange={onChange}
                                        label="tipo"
                                        >
                                        <MenuItem value={0}>
                                            <em>Seleccione un tipo de descuento</em>
                                        </MenuItem>
                                        {listTipoDescuento.map((value,index)=> ( <MenuItem key={index + 1} value={value.idTipoAplicaciones}>{value.descripcion.toUpperCase()}</MenuItem> ))}  
                                    </Select>
                               {/*  <FormHelperText>{!errorIdTipoDescuento&&'Seleccione un tipo'}</FormHelperText>  */}
                                </FormControl>
                            </Grid> 
                            <Grid item xs={6}>
                                     <TextField                            
                                        label=" Nombre Producto"
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
