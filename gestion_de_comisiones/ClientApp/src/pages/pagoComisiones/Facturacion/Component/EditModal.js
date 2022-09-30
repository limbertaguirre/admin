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


const EditModal = (props) => {
   const { open , handleCloseConfirmEdit, handleCloseCancelEdit, listEmpresas, idDetalleEmpresaSelected, idEmpresaSelected, montoSelected, onChangeregistroEdit, nroAutorizacionSelected }= props;
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
                                <b>DETALLE DE LA COMISION </b>
                            </Typography>                            
                           
                            <br />                                                                    
                        </div>   
                        <Grid  container  >
                            <Grid item xs={6}>
                                <FormControl  variant="outlined"  
                                fullWidth  
                                //error={CiudadError} 
                                className={classes.TextFiel}
                                >
                                <InputLabel id="demo-simple-select-outlined-empresa">Empresa</InputLabel>
                                <Select
                                    labelId="demo-simple-select-outlined-labelempresa"                                    
                                   // id="demo-simple-select-outlined"
                                    value={idEmpresaSelected}
                                    name="idEmpresaSelected"
                                    onChange={onChangeregistroEdit}
                                    label=" Empresa"
                                    >
                                    <MenuItem value={0}>
                                        <em>Seleccione una empresa</em>
                                    </MenuItem>
                                    {listEmpresas.map((value,index)=> ( <MenuItem key={index} value={value.idEmpresa}>{value.nombre}</MenuItem> ))}  
                                </Select>
                                {/*  <FormHelperText>{sucursalError&&'Seleccione una ciudad'}</FormHelperText> */}
                            </FormControl>

                            </Grid>
                            <Grid item xs={6}>
                                        <TextField                            
                                        label=" Monto"
                                        type={'number'}
                                        variant="outlined"                                      
                                        name="montoSelected"
                                        value={montoSelected}
                                        
                                        className={classes.TextFiel}
                                        onChange={onChangeregistroEdit}
                                    // error={corporativoError}
                                    /*  helperText={ corporativoError &&
                                        "campo requerido"
                                        }   */                          
                                        fullWidth                             
                                    />


                            </Grid>
                            <Grid item xs={12}> 
                                <Typography variant="subtitle1" className={classes.TextFiel} gutterBottom>
                                   <b>  {'DETALLE DE LA FACTURA'} </b>
                                </Typography>
                            </Grid>
                            <Grid item xs={12}>
                                        <TextField                            
                                        label="Nro Autorizacion"
                                        type={'text'}
                                        variant="outlined"                                      
                                        name="nroAutorizacionSelected"
                                        value={nroAutorizacionSelected}                                        
                                        className={classes.TextFiel}
                                        onChange={onChangeregistroEdit}
                                    // error={corporativoError}
                                    /*  helperText={ corporativoError &&
                                        "campo requerido"
                                        }   */                          
                                        fullWidth                             
                                    />
                            </Grid>

                        </Grid>                
                    <Grid  container item xs={12}  justify="flex-end"  >
                        <Button onClick={handleCloseCancelEdit} variant="contained" color="primary" className={classes.botones} >
                            Cancelar
                        </Button>
                        <Button onClick={handleCloseConfirmEdit} variant="contained" color="primary" className={classes.botones}>
                            Aceptar
                        </Button>
                    </Grid>
            </DialogContent>             
        </Dialog>
    </>
  );
};
export default EditModal;
